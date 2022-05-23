using IfCommerce.Catalog.Application.Contracts.ProductContracts;
using IfCommerce.Catalog.Application.Queries.Parameters;
using IfCommerce.Core.Queries;
using System;
using System.Threading.Tasks;

namespace IfCommerce.Catalog.Application.Interfaces
{
    public interface IProductService
    {
        PagedList<ProductContract> GetPagedProducts(ProductParameters parameters);

        ProductContract GetProductById(Guid id);

        Task AddProduct(AddProductContract contract);
        Task UpdateProduct(Guid id, UpdateProductContract contract);
        Task DeleteProduct(Guid id);
    }
}
