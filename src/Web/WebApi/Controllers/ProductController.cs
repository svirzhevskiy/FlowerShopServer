using Application.Logic;
using Microsoft.AspNetCore.Mvc;
using Models.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _service;

        public ProductController(IProductService service) 
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductModel>>> GetAllByCategory(Guid categoryId)
        {
            return await _service.GetAllByCategory(categoryId);
        }
    }
}
