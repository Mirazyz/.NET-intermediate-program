namespace CatalogService.Domain.Interfaces.Repositories
{
    public interface ICommonRepository
    {
        public ICategoryRepository Category { get; }
        public IItemRepository Item { get; }

        public Task<int> SaveChangesAsync();
    }
}
