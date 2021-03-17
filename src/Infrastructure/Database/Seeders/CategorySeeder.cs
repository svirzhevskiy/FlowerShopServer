using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Seeders
{
    public class CategorySeeder
    {
        public void Seed(AppDbContext context)
        {
            var categories = context.Set<Category>();

            if (categories.Any())
                return;

            categories.AddRange(_categories);
            context.SaveChanges();
        }

        private readonly List<Category> _categories = new()
        {
            new Category { Id = Guid.NewGuid(), Title = "Flowers" },
            new Category { Id = Guid.NewGuid(), Title = "Pots" },
            new Category { Id = Guid.NewGuid(), Title = "Vases" },
        };
    }
}
