using Application.Services;
using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Seeders
{
    public class UserSeeder
    {
        public void Seed(AppDbContext context, IServiceProvider serviceProvider, ILogger<AppDbContext> logger)
        {
            logger.LogInformation("User seed started...");

            try
            {
                var users = context.Set<User>();

                if (users.Any())
                    return;

                var hashService = serviceProvider.GetService(typeof(IHashService)) as IHashService;
                var data = GetUsers(hashService);
                var roles = context.Roles.ToList();
                var userRoleId = roles.First(x => x.Title == "user").Id;

                foreach (var user in data)
                {
                    if (user.Name == "admin")
                        user.RoleId = roles.First(x => x.Title == "admin").Id;
                    else
                        user.RoleId = userRoleId;
                }

                users.AddRange(data);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                logger.LogInformation("User seed finished.");
            }
        }

        private List<User> GetUsers(IHashService hash)
        {
            return new List<User>
            {
                new User { 
                    Id = Guid.NewGuid(), Adress = "Minsk, Lenina 45",
                    Email = "visualstudio@gmail.com", Name = "Larry",
                    Password = hash.GetHash("pass"), Surname = "Dickson",
                    IsDeleted = false
                },
                new User {
                    Id = Guid.NewGuid(), Adress = "Minsk, Lenina 45",
                    Email = "admin", Name = "admin",
                    Password = hash.GetHash("admin"), Surname = "admin",
                    IsDeleted = false
                },
            };
        }
        
    }
}
