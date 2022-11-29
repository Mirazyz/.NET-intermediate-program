using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Interfaces.Repositories
{
    public interface IItemRepository : IRepositoryBase<Item>
    {
        public Task<IEnumerable<Item>> FindAllByCategoryIdAsync(int categoryId);
    }
}
