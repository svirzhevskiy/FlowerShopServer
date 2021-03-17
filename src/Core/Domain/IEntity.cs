using System;

namespace Domain
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}