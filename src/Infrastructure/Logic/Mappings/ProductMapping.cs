using AutoMapper;
using Domain;
using Models.Product;

namespace Logic.Mappings
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductModel>();
        }
    }
}
