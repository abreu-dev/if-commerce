using IfCommerce.Core.Domain;

namespace IfCommerce.Catalog.Domain.Entities
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; set; }
    }
}
