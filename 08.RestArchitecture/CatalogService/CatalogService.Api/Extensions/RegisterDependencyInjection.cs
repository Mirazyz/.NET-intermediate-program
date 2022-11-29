using CatalogService.Domain.Interfaces.Repositories;
using CatalogService.Domain.Interfaces.Services;
using CatalogService.Infrastructure.Persistence;
using CatalogService.Repositories;
using CatalogService.Services;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Api.Extensions
{
    public static class RegisterDependencyInjection
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICommonRepository, CommonRepository>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IItemService, ItemService>();
        }

        public static void RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
