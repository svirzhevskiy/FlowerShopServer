using Application.Logic;
using AutoMapper;
using Database;
using Domain;
using Microsoft.EntityFrameworkCore;
using Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic
{
    public class ProductService : GenericEntityService<Product>, IProductService
    {
        public ProductService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<ProductModel>> GetAllByCategory(Guid categoryId)
        {
            var query = _targetSet.Where(x => !x.IsDeleted && x.CategoryId == categoryId);

            return await _mapper.ProjectTo<ProductModel>(query).ToListAsync();
        }
    }
}
