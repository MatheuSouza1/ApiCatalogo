using ApiCatalogo.Entities;
using AutoMapper;

namespace ApiCatalogo.DTOs.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<ProductsDTO, Product>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
        }
    }
}