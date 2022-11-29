using CatalogService.Domain.DTOs.Category;

namespace CatalogService.Domain.DTOs.Item
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
