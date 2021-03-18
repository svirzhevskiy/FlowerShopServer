using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Seeders
{
    public class OrderSeeder
    {
        public void Seed(AppDbContext context, IServiceProvider serviceProvider, ILogger<AppDbContext> logger)
        {
            logger.LogInformation("Role seed started...");

            try
            {
                var roles = context.Set<Order>();

                if (roles.Any())
                    return;

                var userId = context.Users.First(x => x.Email == "visualstudio@gmail.com").Id;
                var products = context.Products.ToList();

                var currentProduct = 0;

                foreach (var order in _orders)
                {
                    order.UserId = userId;

                    order.Products =
                        new List<Product>
                        {
                            products[currentProduct], products[currentProduct + 1], products[currentProduct + 2]
                        };

                    currentProduct += 3;
                }

                roles.AddRange(_orders);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                logger.LogInformation("Role seed finished.");
            }
        }

        private readonly List<Order> _orders = new()
        {
            new Order { Id = Guid.NewGuid(), IsDeleted = false, OrderDate = new DateTime(2020, 8, 23)},
            new Order { Id = Guid.NewGuid(), IsDeleted = false, OrderDate = new DateTime(2020, 9, 21)},
            new Order { Id = Guid.NewGuid(), IsDeleted = false, OrderDate = new DateTime(2020, 2, 13)},
            new Order { Id = Guid.NewGuid(), IsDeleted = false, OrderDate = new DateTime(2020, 2, 13)},
        };
    }
}
