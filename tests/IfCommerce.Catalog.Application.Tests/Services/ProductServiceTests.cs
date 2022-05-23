using AutoMapper;
using FluentAssertions;
using IfCommerce.Catalog.Application.Contracts.ProductContracts;
using IfCommerce.Catalog.Application.Interfaces;
using IfCommerce.Catalog.Application.Queries.Parameters;
using IfCommerce.Catalog.Application.Services;
using IfCommerce.Catalog.Domain.Commands.ProductCommands;
using IfCommerce.Catalog.Domain.Entities;
using IfCommerce.Catalog.Domain.Interfaces;
using IfCommerce.Core.Mediator;
using IfCommerce.Core.Queries;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IfCommerce.Catalog.Application.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<IMediatorHandler> _mediatorHandler;
        private readonly Mock<IQueryService> _queryService;
        private readonly IProductService _productService;

        public ProductServiceTests()
        {
            _mapper = new Mock<IMapper>();
            _productRepository = new Mock<IProductRepository>();
            _mediatorHandler = new Mock<IMediatorHandler>();
            _queryService = new Mock<IQueryService>();
            _productService = new ProductService(_mapper.Object, _productRepository.Object, _mediatorHandler.Object, _queryService.Object);
        }

        #region GetPagedProducts
        [Fact]
        public void GetPagedProducts_ShouldReturnPagedList()
        {
            // Arrange
            var source = new List<Product>().AsQueryable();

            var parameters = new ProductParameters();

            var entityPagedList = new PagedList<Product>();
            var contractPagedList = new PagedList<ProductContract>();

            _productRepository.Setup(mock => mock.Products()).Returns(source);
            _queryService.Setup(mock => mock.Ordering(source, parameters.Order)).Returns(source);
            _queryService.Setup(mock => mock.Pagination(source, parameters.Page, parameters.Size)).Returns(entityPagedList);
            _mapper.Setup(mock => mock.Map<PagedList<ProductContract>>(entityPagedList)).Returns(contractPagedList);

            // Act
            var result = _productService.GetPagedProducts(parameters);

            // Assert
            _queryService.Verify(mock => mock.Ordering(source, parameters.Order), Times.Once);
            _queryService.Verify(mock => mock.Filtering(source, "Name", parameters.Name), Times.Never);
            _queryService.Verify(mock => mock.Pagination(source, parameters.Page, parameters.Size), Times.Once);
            _mapper.Verify(mock => mock.Map<PagedList<ProductContract>>(entityPagedList), Times.Once);
            result.Should().Be(contractPagedList);
        }

        [Fact]
        public void GetPagedProducts_ShouldReturnPagedList_FilteringByName()
        {
            // Arrange
            var source = new List<Product>().AsQueryable();

            var parameters = new ProductParameters()
            {
                Name = new List<string>() { "Product Name 1" }
            };

            var entityPagedList = new PagedList<Product>();
            var contractPagedList = new PagedList<ProductContract>();

            _productRepository.Setup(mock => mock.Products()).Returns(source);
            _queryService.Setup(mock => mock.Ordering(source, parameters.Order)).Returns(source);
            _queryService.Setup(mock => mock.Filtering(source, "Name", parameters.Name)).Returns(source);
            _queryService.Setup(mock => mock.Pagination(source, parameters.Page, parameters.Size)).Returns(entityPagedList);
            _mapper.Setup(mock => mock.Map<PagedList<ProductContract>>(entityPagedList)).Returns(contractPagedList);

            // Act
            var result = _productService.GetPagedProducts(parameters);

            // Assert
            _queryService.Verify(mock => mock.Ordering(source, parameters.Order), Times.Once);
            _queryService.Verify(mock => mock.Filtering(source, "Name", parameters.Name), Times.Once);
            _queryService.Verify(mock => mock.Pagination(source, parameters.Page, parameters.Size), Times.Once);
            _mapper.Verify(mock => mock.Map<PagedList<ProductContract>>(entityPagedList), Times.Once);
            result.Should().Be(contractPagedList);
        }
        #endregion

        #region GetProductById
        [Fact]
        public void GetProductById_ShouldReturnProduct()
        {
            // Arrange
            var entity = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Name"
            };

            var contract = new ProductContract()
            {
                Id = entity.Id,
                Name = entity.Name
            };

            _productRepository.Setup(mock => mock.GetProductById(entity.Id)).Returns(entity); 
            _mapper.Setup(mock => mock.Map<ProductContract>(entity)).Returns(contract);

            // Act
            var result = _productService.GetProductById(entity.Id);

            // Assert
            _mapper.Verify(mock => mock.Map<ProductContract>(entity), Times.Once);
            result.Should().Be(contract);
        }
        #endregion

        #region AddProduct
        [Fact]
        public void AddProduct_ShouldSendAddProductCommand()
        {
            // Arrange
            var contract = new AddProductContract()
            {
                Name = "Name"
            };

            var entity = new Product()
            {
                Name = contract.Name
            };

            _mapper.Setup(mock => mock.Map<Product>(contract)).Returns(entity);

            // Act
            _productService.AddProduct(contract);

            // Assert
            _mapper.Verify(mock => mock.Map<Product>(contract), Times.Once);
            _mediatorHandler.Verify(mock => mock.SendCommand(It.Is<AddProductCommand>(x => x.Product == entity)), Times.Once);
        }
        #endregion

        #region UpdateProduct
        [Fact]
        public void UpdateProduct_ShouldSendUpdateProductCommand()
        {
            // Arrange
            var contract = new UpdateProductContract()
            {
                Id = Guid.NewGuid(),
                Name = "Name"
            };

            var entity = new Product()
            {
                Id = contract.Id,
                Name = contract.Name
            };

            _mapper.Setup(mock => mock.Map<Product>(contract)).Returns(entity);

            // Act
            _productService.UpdateProduct(contract.Id, contract);

            // Assert
            _mapper.Verify(mock => mock.Map<Product>(contract), Times.Once);
            _mediatorHandler.Verify(mock => mock.SendCommand(It.Is<UpdateProductCommand>(x => x.AggregateId == contract.Id &&
                x.Product == entity)), Times.Once);
        }
        #endregion

        #region DeleteProduct
        [Fact]
        public void DeleteProduct_ShouldSendDeleteProductCommand()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            _productService.DeleteProduct(id);

            // Assert
            _mediatorHandler.Verify(mock => mock.SendCommand(It.Is<DeleteProductCommand>(x => x.AggregateId == id)), Times.Once);
        }
        #endregion
    }
}
