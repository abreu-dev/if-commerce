using FluentAssertions;
using IfCommerce.Catalog.Application.Contracts.ProductContracts;
using System;
using Xunit;

namespace IfCommerce.Catalog.Application.Tests.Contracts.ProductContracts
{
    public class UpdateProductContractTests
    {
        [Fact]
        public void Constructor_ShouldCreateInstance()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Name";

            // Act
            var contract = new UpdateProductContract()
            {
                Id = id,
                Name = name
            };

            // Assert
            contract.Id.Should().Be(id);
            contract.Name.Should().Be(name);
        }
    }
}
