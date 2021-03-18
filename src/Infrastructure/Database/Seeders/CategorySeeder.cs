using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Seeders
{
    public class CategorySeeder
    {
        public void Seed(AppDbContext context, IServiceProvider serviceProvider, ILogger<AppDbContext> logger)
        {
            logger.LogInformation("Category seed started...");

            try
            {
                var categories = context.Set<Category>();

                if (categories.Any())
                    return;

                categories.AddRange(_categories);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                logger.LogInformation("Category seed finished.");
            }
        }

        private readonly List<Category> _categories = new()
        {
            new Category { Id = Guid.NewGuid(), Title = "Flowers" },
            new Category { Id = Guid.NewGuid(), Title = "Pots" },
            new Category { Id = Guid.NewGuid(), Title = "Vases" },
        };
    }
}
