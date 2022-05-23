using FluentAssertions;
using IfCommerce.Catalog.Domain.Entities;
using System;
using Xunit;

namespace IfCommerce.Catalog.Domain.Tests.Entities
{
    public class ProductTests
    {
        [Fact]
        public void Constructor_ShouldCreateInstance()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Name";

            // Act
            var notification = new Product()
            {
                Id = id,
                Name = name
            };

            // Assert
            notification.Id.Should().Be(id);
            notification.Name.Should().Be(name);
        }
    }
}
