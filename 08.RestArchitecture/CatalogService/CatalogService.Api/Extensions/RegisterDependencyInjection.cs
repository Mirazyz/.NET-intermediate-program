using AutoMapper;
using CatalogService.Domain.Interfaces.Repositories;
using CatalogService.Domain.Interfaces.Services;
using CatalogService.Infrastructure.Persistence;
using CatalogService.Infrastructure.Persistence.Interceptors;
using CatalogService.Repositories;
using CatalogService.Services;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Api.Extensions
{
    public static class RegisterDependencyInjection
    {
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICommonRepository, CommonRepository>();

            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
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
