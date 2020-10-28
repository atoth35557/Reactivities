using System;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using(var scope = host.Services.CreateScope()){
            
                try{
                    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                    context.Database.Migrate();
                    Seed.SeedData(context, userManager).Wait();
               }
                catch(Exception e){
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e,"An exception occurred during migration!");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
