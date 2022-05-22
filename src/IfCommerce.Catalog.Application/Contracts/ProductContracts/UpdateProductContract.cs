using System;

namespace IfCommerce.Catalog.Application.Contracts.ProductContracts
{
    public class UpdateProductContract
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
