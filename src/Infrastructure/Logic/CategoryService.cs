using Application.Logic;
using AutoMapper;
using Database;
using Domain;
using Microsoft.EntityFrameworkCore;
using Models.Category;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic
{
    public class CategoryService : GenericEntityService<Category>, ICategoryService
    {
        public CategoryService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<CategoryModel>> GetAll()
        {
            var query = _targetSet.Where(x => !x.IsDeleted);

            return await _mapper.ProjectTo<CategoryModel>(query).ToListAsync();
        }
    }
}
