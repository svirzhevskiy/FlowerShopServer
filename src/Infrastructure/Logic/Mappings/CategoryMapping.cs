using AutoMapper;
using Domain;
using Models.Category;
using System.Linq;

namespace Logic.Mappings
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryModel>()
                .ForMember(x => x.Properties, y => y.MapFrom(s => s.Properties.Select(p => p.Title)));
        }
    }
}
