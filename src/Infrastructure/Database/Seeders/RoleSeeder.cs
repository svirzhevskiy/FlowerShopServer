using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Seeders
{
    public class RoleSeeder
    {
        public void Seed(AppDbContext context, IServiceProvider serviceProvider, ILogger<AppDbContext> logger)
        {
            logger.LogInformation("Role seed started...");

            try
            {
                var roles = context.Set<Role>();

                if (roles.Any())
                    return;

                roles.AddRange(_roles);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                logger.LogInformation("Role seed finished.");
            }
        }

        private readonly List<Role> _roles = new()
        {
            new Role { Id = Guid.NewGuid(), Title = "user" },
            new Role { Id = Guid.NewGuid(), Title = "admin" },
        };
    }
}
