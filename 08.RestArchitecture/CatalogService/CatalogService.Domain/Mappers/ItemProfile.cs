using AutoMapper;
using CatalogService.Domain.DTOs.Item;
using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Mappers
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDto>();
            CreateMap<ItemDto, Item>();
            CreateMap<ItemForCreateDto, Item>();
            CreateMap<ItemForUpdateDto, Item>();
        }
    }
}
