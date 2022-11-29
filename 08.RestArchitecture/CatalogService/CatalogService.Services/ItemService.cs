using AutoMapper;
using CatalogService.Domain.DTOs.Item;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces.Repositories;
using CatalogService.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Services
{
    public class ItemService : IItemService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public ItemService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemDto>> GetAllItemsAsync(int pageSize, int pageNumber)
        {
            try
            {
                var categories = await _repository.Item.FindAllAsync(pageSize, pageNumber);

                var ItemDtos = _mapper.Map<IEnumerable<ItemDto>>(categories);

                return ItemDtos;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ItemDto>> GetAllItemsByCategoryIdAsync(int categoryId)
        {
            try
            {
                var items = await _repository.Item.FindAllByCategoryIdAsync(categoryId);

                var itemDtos = _mapper.Map<IEnumerable<ItemDto>>(items);

                return itemDtos;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ItemDto> GetItemByIdAsync(int id)
        {
            try
            {
                var Item = await _repository.Item.FindByIdAsync(id);

                var ItemDto = _mapper.Map<ItemDto>(Item);

                return ItemDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ItemDto> CreateItemAsync(ItemForCreateDto ItemToCreate)
        {
            try
            {
                var ItemEntity = _mapper.Map<Item>(ItemToCreate);

                var createdEntity = _repository.Item.Create(ItemEntity);
                await _repository.SaveChangesAsync();

                var ItemDto = _mapper.Map<ItemDto>(createdEntity);

                return ItemDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateItemAsync(ItemForUpdateDto ItemToUpdate)
        {
            try
            {
                var ItemEntity = _mapper.Map<Item>(ItemToUpdate);

                _repository.Item.Update(ItemEntity);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteItemAsync(int id)
        {
            try
            {
                _repository.Item.Delete(id);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

    }
}
