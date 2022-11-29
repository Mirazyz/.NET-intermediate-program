using Bogus;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using SQLitePCL;

namespace CatalogService.Api.Extensions
{
    public static class DatabaseInitializer
    {
        private static readonly Faker _faker = new();
        private static readonly Random _random = new();

        public static void SeedDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();

                CreateCategories(context);
                CreateItems(context);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private static void CreateCategories(ApplicationDbContext context)
        {
            if (context.Categories.Any()) return;

            List<Category> categories = new List<Category>();

            var randomCategories = _faker.Commerce.Categories(50);

            foreach(var randomCategory in randomCategories)
            {
                categories.Add(new Category(randomCategory));
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private static void CreateItems(ApplicationDbContext context)
        {
            if (context.Items.Any()) return;

            List<Item> items = new List<Item>();
            var categories = context.Categories.ToList();

            foreach(var category in categories)
            {
                var numberOfItems = _random.Next(10, 100);

                for(int i = 0; i < numberOfItems; i++)
                {
                    var itemName = _faker.Commerce.ProductName();
                    items.Add(new Item(itemName, category.Id));
                }
            }

            context.Items.AddRange(items);
            context.SaveChanges();
        }
    }
}
