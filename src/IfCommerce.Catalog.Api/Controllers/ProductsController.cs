using IfCommerce.Catalog.Application.Contracts.ProductContracts;
using IfCommerce.Catalog.Application.Interfaces;
using IfCommerce.Catalog.Application.Query.Parameters;
using IfCommerce.Core.Messaging.Notifications;
using IfCommerce.Core.Query;
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

        [HttpGet]
        [Route("products:paginated")]
        public PagedList<ProductContract> GetPagedProducts([FromQuery] ProductParameters parameters)
        {
            return _productService.GetPagedProducts(parameters);
        }

        [HttpGet]
        [Route("products/{id:guid}")]
        public ProductContract Get(Guid id)
        {
            return _productService.GetProductById(id);
        }

        [HttpPost]
        [Route("products")]
        public async Task<IActionResult> Post([FromBody] AddProductContract contract)
        {
            await _productService.AddProduct(contract);
            return CustomResponse("/products");
        }

        [HttpPut]
        [Route("products/{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateProductContract contract)
        {
            await _productService.UpdateProduct(id, contract);
            return CustomResponse("/products");
        }

        [HttpDelete]
        [Route("products/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productService.DeleteProduct(id);
            return CustomResponse("/products");
        }
    }
}
