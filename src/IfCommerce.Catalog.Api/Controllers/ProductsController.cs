using IfCommerce.Catalog.Application.Contracts.ProductContracts;
using IfCommerce.Catalog.Application.Interfaces;
using IfCommerce.Catalog.Application.Queries.Parameters;
using IfCommerce.Core.Messaging.Notifications;
using IfCommerce.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IfCommerce.Catalog.Api.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(INotificationHandler<DomainNotification> notifications,
                                  IProductService productService)
            : base(notifications)
        {
            _productService = productService;
        }

        [HttpGet("[controller]:paginated")]
        public PagedList<ProductContract> GetPagedProducts([FromQuery] ProductParameters parameters)
        {
            return _productService.GetPagedProducts(parameters);
        }

        [HttpGet("[controller]/{id:guid}")]
        public ProductContract Get(Guid id)
        {
            return _productService.GetProductById(id);
        }

        [HttpPost("[controller]")]
        public async Task<IActionResult> Post([FromBody] AddProductContract contract)
        {
            await _productService.AddProduct(contract);
            return CustomResponse();
        }

        [HttpPut("[controller]/{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateProductContract contract)
        {
            await _productService.UpdateProduct(id, contract);
            return CustomResponse();
        }

        [HttpDelete("[controller]/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productService.DeleteProduct(id);
            return CustomResponse();
        }
    }
}
