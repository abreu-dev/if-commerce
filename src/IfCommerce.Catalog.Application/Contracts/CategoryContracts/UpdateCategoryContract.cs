using System;

namespace IfCommerce.Catalog.Application.Contracts.CategoryContracts
{
    public class UpdateCategoryContract
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
