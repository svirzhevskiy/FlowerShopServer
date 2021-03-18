using System;
using System.Collections.Generic;

namespace Domain
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Adress { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}