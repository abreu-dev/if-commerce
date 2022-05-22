using IfCommerce.Catalog.Domain.Entities;
using IfCommerce.Core.Data;
using System;
using System.Linq;

namespace IfCommerce.Catalog.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> Products();

        Product GetProductById(Guid id);

        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
