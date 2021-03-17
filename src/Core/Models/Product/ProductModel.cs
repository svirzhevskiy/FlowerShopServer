using System;

namespace Models.Product
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Properties { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int? OldPrice { get; set; }
    }
}
