using CatalogService.Domain.Interfaces.Repositories;
using CatalogService.Infrastructure.Persistence;

namespace CatalogService.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private ApplicationDbContext _context;

        private readonly ICategoryRepository _category;
        public ICategoryRepository Category => _category ??
            new CategoryRepository(_context);

        private readonly IItemRepository _item;
        public IItemRepository Item => _item ??
            new ItemRepository(_context);

        public CommonRepository(ApplicationDbContext context, ICategoryRepository category, IItemRepository item)
        {
            _context = context;

            _category = category;
            _item = item;
        }

        public async Task<int> SaveChangesAsync()
        {
            int savedChanges = await _context.SaveChangesAsync();

            return savedChanges;
        }
    }
}
