using Domain;
using System;
using System.Threading.Tasks;

namespace Application.Logic
{
    public interface IGenericEntityService<TEntity> where TEntity : IEntity
    {
        public Task Delete(Guid id);
    }
}
