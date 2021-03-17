using System;

namespace Domain
{
    public class Product : IEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Properties { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int? OldPrice { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}