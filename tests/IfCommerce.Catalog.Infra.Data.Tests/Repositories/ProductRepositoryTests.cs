using FluentAssertions;
using IfCommerce.Catalog.Domain.Entities;
using IfCommerce.Catalog.Domain.Interfaces;
using IfCommerce.Catalog.Infra.Data.Context;
using IfCommerce.Catalog.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IfCommerce.Catalog.Infra.Data.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        private readonly Mock<ICatalogContext> _catalogContext;
        private readonly IProductRepository _productRepository;

        public ProductRepositoryTests()
        {
            _catalogContext = new Mock<ICatalogContext>();
            _productRepository = new ProductRepository(_catalogContext.Object);
        }

        #region UnitOfWork
        [Fact]
        public void UnitOfWork_ShouldReturnCatalogContext()
        {
            // Arrange & Act
            var result = _productRepository.UnitOfWork;

            // Assert
            result.Should().BeEquivalentTo(_catalogContext.Object);
        }
        #endregion

        #region Products
        [Fact]
        public void Products_ShouldReturnDbSet()
        {
            // Arrange
            var mockDbSet = GetMockDbSet(new List<Product>());
            _catalogContext.Setup(mock => mock.Products).Returns(mockDbSet.Object);

            // Act
            var result = _productRepository.Products();

            // Assert
            result.Should().BeEquivalentTo(mockDbSet.Object.AsQueryable());
        }
        #endregion

        #region GetProductById
        [Fact]
        public void GetProductById_ShouldReturnProduct_WhenFound()
        {
            // Arrange
            var product = new Product() { Id = Guid.NewGuid() };
            var mockDbSet = GetMockDbSet(new List<Product>() { product });
            _catalogContext.Setup(mock => mock.Products).Returns(mockDbSet.Object);

            // Act
            var result = _productRepository.GetProductById(product.Id);

            // Assert
            result.Should().Be(product);
        }

        [Fact]
        public void GetProductById_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            var product = new Product() { Id = Guid.NewGuid() };
            var mockDbSet = GetMockDbSet(new List<Product>() { product });
            _catalogContext.Setup(mock => mock.Products).Returns(mockDbSet.Object);

            // Act
            var result = _productRepository.GetProductById(Guid.NewGuid());

            // Assert
            result.Should().BeNull();
        }
        #endregion

        #region AddProduct
        [Fact]
        public void AddProduct_ShouldAddToDbSet()
        {
            // Arrange
            var mockDbSet = GetMockDbSet(new List<Product>());
            _catalogContext.Setup(mock => mock.Products).Returns(mockDbSet.Object);
            var product = new Product();

            // Act
            _productRepository.AddProduct(product);

            // Assert
            mockDbSet.Verify(mock => mock.Add(product), Times.Once);
        }
        #endregion

        #region UpdateProduct
        [Fact]
        public void UpdateProduct_ShouldUpdateToDbSet()
        {
            // Arrange
            var mockDbSet = GetMockDbSet(new List<Product>());
            _catalogContext.Setup(mock => mock.Products).Returns(mockDbSet.Object);
            var product = new Product();

            // Act
            _productRepository.UpdateProduct(product);

            // Assert
            mockDbSet.Verify(mock => mock.Update(product), Times.Once);
        }
        #endregion

        #region DeleteProduct
        [Fact]
        public void DeleteProduct_ShouldRemoveToDbSet()
        {
            // Arrange
            var mockDbSet = GetMockDbSet(new List<Product>());
            _catalogContext.Setup(mock => mock.Products).Returns(mockDbSet.Object);
            var product = new Product();

            // Act
            _productRepository.DeleteProduct(product);

            // Assert
            mockDbSet.Verify(mock => mock.Remove(product), Times.Once);
        }
        #endregion

        private static Mock<DbSet<T>> GetMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet;
        }
    }
}
