using IfCommerce.Core.Domain;

namespace IfCommerce.Catalog.Domain.Entities
{
    public class Category : Entity, IAggregateRoot
    {
        public string Name { get; set; }
    }
}
