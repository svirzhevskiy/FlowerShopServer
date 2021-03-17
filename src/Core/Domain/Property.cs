using System;
using System.Collections.Generic;

namespace Domain
{
    public class Property : IEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        
        public IEnumerable<Category> Categories { get; set; }
    }
}