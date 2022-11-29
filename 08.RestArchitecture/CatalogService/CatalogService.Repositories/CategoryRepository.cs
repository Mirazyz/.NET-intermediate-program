using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces.Repositories;
using CatalogService.Infrastructure.Persistence;

namespace CatalogService.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
