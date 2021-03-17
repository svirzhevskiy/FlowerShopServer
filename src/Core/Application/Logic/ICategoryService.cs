using Domain;
using Models.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Logic
{
    public interface ICategoryService : IGenericEntityService<Category>
    {
        public Task<List<CategoryModel>> GetAll();
    }
}
