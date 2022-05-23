using FluentAssertions;
using IfCommerce.Core.Queries;
using Xunit;

namespace IfCommerce.Core.Tests.Queries
{
    public class QueryParametersTests
    {
        public class ConcreteQueryParameters : QueryParameters { }

        [Fact]
        public void Constructor_ShouldCreateInstance()
        {
            // Arrange
            var page = 5;
            var size = 20;
            var order = "Property";

            // Act
            var parameters = new ConcreteQueryParameters()
            {
                Page = page,
                Size = size,
                Order = order
            };

            // Assert
            parameters.Page.Should().Be(page);
            parameters.Size.Should().Be(size);
            parameters.Order.Should().Be(order);
        }

        [Fact]
        public void Page_ShouldBeSettedToOne_WhenValueLessThanOne()
        {
            // Arrange
            var page = 0;

            // Act
            var parameters = new ConcreteQueryParameters()
            {
                Page = page
            };

            // Assert
            parameters.Page.Should().Be(1);
        }

        [Fact]
        public void Size_ShouldBeSettedToFifty_WhenValueLessThanOne()
        {
            // Arrange
            var size = 0;

            // Act
            var parameters = new ConcreteQueryParameters()
            {
                Size = size
            };

            // Assert
            parameters.Size.Should().Be(50);
        }

        [Fact]
        public void Size_ShouldBeSettedToFifty_WhenValueGreatherThanFifty()
        {
            // Arrange
            var size = 51;

            // Act
            var parameters = new ConcreteQueryParameters()
            {
                Size = size
            };

            // Assert
            parameters.Size.Should().Be(50);
        }
    }
}
