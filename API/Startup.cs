using System;
using System.Text;
using System.Threading.Tasks;
using Application.Activities;
using Application.Interfaces;
using Application.Profiles;
using API.Middleware;
using API.SignalR;
using AutoMapper;
using Domain;
using FluentValidation.AspNetCore;
using Infrastructure.Interfaces;
using Infrastructure.Photos;
using Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace API {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration {
            get;
        }

        public void ConfigureDevelopmentServices (IServiceCollection services) {
            services.AddDbContext<DataContext> (options => {
                options.UseLazyLoadingProxies ();
                options.UseSqlite (Configuration.GetConnectionString ("DefaultConnection"));
            });
            ConfigureServices(services);
        }
        public void ConfigureProductionServices (IServiceCollection services) {
            services.AddDbContext<DataContext> (options => {
                options.UseLazyLoadingProxies ();
                options.UseMySql (Configuration.GetConnectionString ("DefaultConnection"));
            });
            ConfigureServices(services);
        }

        public void ConfigureServices (IServiceCollection services) {

            services.AddCors (options => {
                options.AddPolicy ("CorsPolicy", builder => {
                    builder.WithOrigins ("http://localhost:3000")
                        .SetIsOriginAllowedToAllowWildcardSubdomains ()
                        .AllowAnyHeader ()
                        .AllowCredentials ()
                        .WithExposedHeaders ("WWW-Authenticate")
                        .AllowAnyMethod ();
                });
            });
            services.AddMediatR (typeof (List.Handler).Assembly);

            services.AddAutoMapper (typeof (List.Handler));

            services.AddSignalR ();

            services.AddControllers (
                    opt => {
                        var policy = new AuthorizationPolicyBuilder ().RequireAuthenticatedUser ().Build ();
                        opt.Filters.Add (new AuthorizeFilter (policy));
                    }
                )
                .AddFluentValidation (config => config.RegisterValidatorsFromAssemblyContaining<Create> ());

            var builder = services.AddIdentityCore<AppUser> ();
            var identityBuilder = new IdentityBuilder (builder.UserType, builder.Services);
            identityBuilder
                .AddEntityFrameworkStores<DataContext> ()
                .AddSignInManager<SignInManager<AppUser>> ();

            services.AddAuthorization (ops => {
                ops.AddPolicy ("IsActivityHost", policy => {
                    policy.Requirements.Add (new IsHostRequirement ());
                });
            });

            services.AddTransient<IAuthorizationHandler, IsHostRequirementHandler> ();

            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (Configuration["TokenKey"]));
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (opt => {
                    opt.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                    };
                    opt.Events = new JwtBearerEvents {
                        OnMessageReceived = context => {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty (accessToken) && (path.StartsWithSegments ("/chat"))) {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddScoped<IJwtGenerator, JwtGenerator> ();
            services.AddScoped<IUserAccessor, UserAccessor> ();
            services.AddScoped<IProfileReader, ProfileReader> ();
            services.AddScoped<IPhotoAccessor, PhotoAccessor> ();
            services.Configure<CloudinarySettings> (Configuration.GetSection ("Cloudenary"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {

            app.UseMiddleware<ErrorHandlingMiddleware> ();
            if (env.IsDevelopment ()) {
                // app.UseDeveloperExceptionPage ();
            }

            // app.UseHttpsRedirection();
            app.UseDefaultFiles ();
            app.UseStaticFiles ();

            app.UseRouting ();
            app.UseCors ("CorsPolicy");

            app.UseAuthentication ();
            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
                endpoints.MapHub<ChatHub> ("/chat");
                endpoints.MapFallbackToController ("Index", "Fallback");
            });
        }
    }
}