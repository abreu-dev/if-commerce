using FluentAssertions;
using IfCommerce.Catalog.Application.Query.Parameters;
using IfCommerce.Core.Query;
using System.Collections.Generic;
using Xunit;

namespace IfCommerce.Catalog.Application.Tests.Queries.Parameters
{
    public class ProductParametersTests
    {
        [Fact]
        public void Constructor_ShouldCreateInstance()
        {
            // Arrange
            var order = "Property";
            var name = new List<string>() { "Product Name 1", "Product Name 2" };

            // Act
            var parameters = new ProductParameters()
            {
                Order = order,
                Name = name
            };

            // Assert
            parameters.Order.Should().Be(order);
            parameters.Name.Should().BeEquivalentTo(name);
            parameters.Should().BeAssignableTo<QueryParameters>();
        }

        [Fact]
        public void Constructor_ShouldCreateInstance_WithDefaultValues()
        {
            // Arrange & Act
            var parameters = new ProductParameters();

            // Assert
            parameters.Order.Should().Be("Name");
            parameters.Name.Should().BeEmpty();
            parameters.Should().BeAssignableTo<QueryParameters>();
        }
    }
}
