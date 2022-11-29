using AutoMapper;
using CatalogService.Domain.DTOs.Category;
using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<CategoryForCreateDto, Category>();
            CreateMap<CategoryForUpdateDto, Category>();
        }
    }
}
