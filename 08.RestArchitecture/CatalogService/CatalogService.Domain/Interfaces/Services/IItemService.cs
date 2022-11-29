using CatalogService.Domain.DTOs.Item;

namespace CatalogService.Domain.Interfaces.Services
{
    public interface IItemService
    {
        public Task<IEnumerable<ItemDto>> GetAllItemsAsync(int pageSize, int pageNumber);
        public Task<IEnumerable<ItemDto>> GetAllItemsByCategoryIdAsync(int categoryId);
        public Task<ItemDto> GetItemByIdAsync(int id);
        public Task<ItemDto> CreateItemAsync(ItemForCreateDto itemToCreate);
        public Task UpdateItemAsync(ItemForUpdateDto itemToUpdate);
        public Task DeleteItemAsync(int id);
    }
}
