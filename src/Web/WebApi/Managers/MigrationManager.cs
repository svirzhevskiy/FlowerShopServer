using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace WebApi.Managers
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, IEnumerable<Action<TContext, IServiceProvider, ILogger<TContext>>> seeders)
            where TContext : DbContext
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<TContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();

            try
            {
                context.Database.Migrate();

                foreach (var seeder in seeders)
                {
                    seeder(context, services, logger);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                context.Dispose();
            }

            return host;
        }
    }
}
