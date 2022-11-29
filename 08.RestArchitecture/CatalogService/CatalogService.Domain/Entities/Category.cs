using CatalogService.Domain.Common;

namespace CatalogService.Domain.Entities
{
    public class Category : BaseAuditableEntity
    {
        public string Name { get; set; }

        public ICollection<Item> Items { get; set; }

        public Category(string name)
        {
            Name = name;
            Items = new List<Item>();
        }
    }
}
