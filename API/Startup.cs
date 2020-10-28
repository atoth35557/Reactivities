using API.Middleware;
using Application.Activities;
using Domain;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;

namespace API {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            // using Microsoft.EntityFrameworkCore;
            services.AddDbContext<DataContext> (options =>
                options.UseSqlite (Configuration.GetConnectionString ("DefaultConnection")));
            services.AddCors (options => {
                options.AddPolicy ("CorsPolicy", builder => {
                    builder.WithOrigins ("http://localhost:3000")
                        .SetIsOriginAllowedToAllowWildcardSubdomains ()
                        .AllowAnyHeader ()
                        .AllowAnyMethod ();
                });
            });
            services.AddMediatR (typeof (List.Handler).Assembly);

            services.AddControllers ()
                .AddFluentValidation (config => config.RegisterValidatorsFromAssemblyContaining<Create> ());
            
            var builder = services.AddIdentityCore<AppUser>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder
                .AddEntityFrameworkStores<DataContext>()
                .AddSignInManager<SignInManager<AppUser>>();
            services.AddAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
           
           app.UseMiddleware<ErrorHandlingMiddleware>();
            if (env.IsDevelopment ()) {
                // app.UseDeveloperExceptionPage ();
            }

            // app.UseHttpsRedirection();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseCors ("CorsPolicy");

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}