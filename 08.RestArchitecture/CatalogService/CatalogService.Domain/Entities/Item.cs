using CatalogService.Domain.Common;

namespace CatalogService.Domain.Entities
{
    public class Item : BaseAuditableEntity
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Item(string name, int categoryId)
        {
            Name = name;
            CategoryId = categoryId;
        }
    }
}
