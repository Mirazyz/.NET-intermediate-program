using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces.Repositories;
using CatalogService.Infrastructure.Persistence;

namespace CatalogService.Repositories
{
    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
