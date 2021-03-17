using Database;
using Database.Seeders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using WebApi.Managers;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var seeders = new List<Action<AppDbContext>>
            {
                context => new CategorySeeder().Seed(context),
                context => new ProductSeeder().Seed(context),
            };

            CreateHostBuilder(args).Build().MigrateDatabase(seeders).Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
