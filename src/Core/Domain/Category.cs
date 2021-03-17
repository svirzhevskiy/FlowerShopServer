using System;
using System.Collections.Generic;

namespace Domain
{
    public class Category : IEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        
        public IEnumerable<Property> Properties { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}