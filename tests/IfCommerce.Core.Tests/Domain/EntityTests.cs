using IfCommerce.Core.Domain;
using System;
using Xunit;

namespace IfCommerce.Core.Tests.Domain
{
    public class EntityTests
    {
        public class ConcreteEntity : Entity { }

        [Fact]
        public void Constructor_ShouldCreateInstance()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var entity = new ConcreteEntity()
            {
                Id = id
            };

            // Assert
            Assert.Equal(id, entity.Id);
        }
    }
}
