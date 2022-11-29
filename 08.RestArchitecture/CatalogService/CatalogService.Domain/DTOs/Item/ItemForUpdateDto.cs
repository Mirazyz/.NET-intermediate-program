namespace CatalogService.Domain.DTOs.Item
{
    public class ItemForUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
    }
}
