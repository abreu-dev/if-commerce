using FluentAssertions;
using IfCommerce.Catalog.Application.Contracts.ProductContracts;
using Xunit;

namespace IfCommerce.Catalog.Application.Tests.Contracts.ProductContracts
{
    public class AddProductContractTests
    {
        [Fact]
        public void Constructor_ShouldCreateInstance()
        {
            // Arrange
            var name = "Name";

            // Act
            var contract = new AddProductContract()
            {
                Name = name
            };

            // Assert
            contract.Name.Should().Be(name);
        }
    }
}
