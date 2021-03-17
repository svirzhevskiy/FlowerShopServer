using Application.Exceptions;
using Application.Logic;
using AutoMapper;
using Database;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Logic
{
    public class GenericEntityService<TEntity> : IGenericEntityService<TEntity> where TEntity : class, IEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _targetSet;
        protected readonly IMapper _mapper;

        public GenericEntityService(AppDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException();
            _targetSet = _context.Set<TEntity>();
            _mapper = mapper;
        }

        public async Task Delete(Guid id)
        {
            var entity = await _targetSet.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (entity == null)
                throw AppError.NotFound;

            _targetSet.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}
