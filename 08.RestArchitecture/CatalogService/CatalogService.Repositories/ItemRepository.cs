using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces.Repositories;
using CatalogService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Repositories
{
    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Item>> FindAllByCategoryIdAsync(int categoryId)
        {
            var items = await _context.Items
                .Where(i => i.CategoryId == categoryId)
                .ToListAsync();

            return items;
        }
    }
}
