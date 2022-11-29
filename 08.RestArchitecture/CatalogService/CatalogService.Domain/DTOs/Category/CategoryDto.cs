using CatalogService.Domain.DTOs.Item;

namespace CatalogService.Domain.DTOs.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ItemDto> Items { get; set; }
    }
}
