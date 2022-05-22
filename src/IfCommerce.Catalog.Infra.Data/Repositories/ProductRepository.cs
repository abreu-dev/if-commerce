using IfCommerce.Catalog.Domain.Entities;
using IfCommerce.Catalog.Domain.Interfaces;
using IfCommerce.Catalog.Infra.Data.Context;
using IfCommerce.Core.Data;
using System;
using System.Linq;

namespace IfCommerce.Catalog.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _catalogContext;

        public IUnitOfWork UnitOfWork => _catalogContext;

        public ProductRepository(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public IQueryable<Product> Products()
        {
            return _catalogContext.Products;
        }

        public Product GetProductById(Guid id)
        {
            return _catalogContext.Products.SingleOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            _catalogContext.Products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            _catalogContext.Products.Update(product);
        }

        public void DeleteProduct(Product product)
        {
            _catalogContext.Products.Remove(product);
        }
    }
}
