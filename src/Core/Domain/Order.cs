using System;
using System.Collections.Generic;

namespace Domain
{
    public class Order : IEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime OrderDate { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}