using AutoMapper;
using IfCommerce.Catalog.Application.Contracts.ProductContracts;
using IfCommerce.Catalog.Application.Helpers;
using IfCommerce.Catalog.Application.Interfaces;
using IfCommerce.Catalog.Application.Parameters;
using IfCommerce.Catalog.Application.Responses;
using IfCommerce.Catalog.Domain.Commands.ProductCommands;
using IfCommerce.Catalog.Domain.Entities;
using IfCommerce.Catalog.Domain.Interfaces;
using IfCommerce.Core.Mediator;
using System;
using System.Collections.Generic;
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

        public ProductService(IMapper mapper, IProductRepository productRepository, IMediatorHandler mediatorHandler)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _mediatorHandler = mediatorHandler;
        }

        public PagedList<ProductContract> GetPagedProducts(ProductParameters parameters)
        {
            var source = _productRepository.Products();

            source = string.IsNullOrEmpty(parameters.Order) ? source.OrderBy(p => p.Name) : source.OrderBy(parameters.Order);

            if (parameters.Name.Any())
            {
                source = source.ApplyFilter("Name", parameters.Name);
            }

            var totalItems = source.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)parameters.Size);
            var items = _mapper.Map<IEnumerable<ProductContract>>(source
                .Skip(parameters.Page * parameters.Size)
                .Take(parameters.Size)
                .ToList());

            return new PagedList<ProductContract>(items, parameters.Page, totalItems, totalPages);
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
