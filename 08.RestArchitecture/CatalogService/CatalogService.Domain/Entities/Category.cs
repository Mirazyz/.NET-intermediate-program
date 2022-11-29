using CatalogService.Domain.Common;

namespace CatalogService.Domain.Entities
{
    public class Category : BaseAuditableEntity
    {
        public int Id { get; set; }
        public int MyProperty { get; set; }
    }
}
