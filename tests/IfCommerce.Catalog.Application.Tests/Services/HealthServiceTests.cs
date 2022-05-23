using FluentAssertions;
using IfCommerce.Catalog.Application.Interfaces;
using IfCommerce.Catalog.Application.Services;
using IfCommerce.Catalog.Infra.Data.Context;
using Moq;
using Xunit;

namespace IfCommerce.Catalog.Application.Tests.Services
{
    public class HealthServiceTests
    {
        private readonly Mock<ICatalogContext> _catalogContext;
        private readonly IHealthService _healthService;

        public HealthServiceTests()
        {
            _catalogContext = new Mock<ICatalogContext>();
            _healthService = new HealthService(_catalogContext.Object);
        }

        [Fact]
        public void IsHealthy_ShouldReturnContextIsAvailable()
        {
            // Arrange
            _catalogContext.Setup(mock => mock.IsAvailable()).Returns(true);

            // Arrange & Act
            var isHealthy = _healthService.IsHealthy();
            
            // Assert
            _catalogContext.Verify(mock => mock.IsAvailable(), Times.Once);
            isHealthy.Should().BeTrue();
        }
    }
}
