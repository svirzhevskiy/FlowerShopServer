using System;
using System.Collections.Generic;

namespace Domain
{
    public class Role : IEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}
