using Domain;
using Models.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Logic
{
    public interface IProductService : IGenericEntityService<Product>
    {
        public Task<List<ProductModel>> GetAllByCategory(Guid categoryId);
    }
}
