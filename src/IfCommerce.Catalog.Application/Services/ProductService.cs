using AutoMapper;
using IfCommerce.Catalog.Application.Contracts.ProductContracts;
using IfCommerce.Catalog.Application.Interfaces;
using IfCommerce.Catalog.Application.Query.Parameters;
using IfCommerce.Catalog.Domain.Commands.ProductCommands;
using IfCommerce.Catalog.Domain.Entities;
using IfCommerce.Catalog.Domain.Interfaces;
using IfCommerce.Core.Mediator;
using IfCommerce.Core.Query;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IfCommerce.Catalog.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IQueryService _queryService;

        public ProductService(IMapper mapper,
                              IProductRepository productRepository,
                              IMediatorHandler mediatorHandler, 
                              IQueryService queryService)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _mediatorHandler = mediatorHandler;
            _queryService = queryService;
        }

        public PagedList<ProductContract> GetPagedProducts(ProductParameters parameters)
        {
            var source = _productRepository.Products();

            source = _queryService.Ordering(source, parameters.Order);

            if (parameters.Name.Any())
            {
                source = _queryService.Filtering(source, "Name", parameters.Name);
            }

            var pagedList = _queryService.Pagination(source, parameters.Page, parameters.Size);
            return _mapper.Map<PagedList<ProductContract>>(pagedList);
        }

        public ProductContract GetProductById(Guid id)
        {
            return _mapper.Map<ProductContract>(_productRepository.GetProductById(id));
        }

        public async Task AddProduct(AddProductContract contract)
        {
            var command = new AddProductCommand()
            {
                Product = _mapper.Map<Product>(contract)
            };

            await _mediatorHandler.SendCommand(command);
        }

        public async Task UpdateProduct(Guid id, UpdateProductContract contract)
        {
            var command = new UpdateProductCommand(id)
            {
                Product = _mapper.Map<Product>(contract)
            };

            await _mediatorHandler.SendCommand(command);
        }

        public async Task DeleteProduct(Guid id)
        {
            var command = new DeleteProductCommand(id);

            await _mediatorHandler.SendCommand(command);
        }
    }
}
