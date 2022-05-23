using FluentAssertions;
using IfCommerce.Core.Domain;
using IfCommerce.Core.Queries;
using System.Collections.Generic;
using Xunit;

namespace IfCommerce.Core.Tests.Query
{
    public class PagedListTests
    {
        public class ConcreteEntity : Entity { }

        [Fact]
        public void Constructor_ShouldCreateInstance()
        {
            // Arrange
            var items = new List<ConcreteEntity>();
            var count = 30;
            var page = 5;
            var size = 10;

            // Act
            var pagedList = new PagedList<ConcreteEntity>(items, count, page, size);

            // Assert
            pagedList.Data.Should().BeEquivalentTo(items);
            pagedList.CurrentPage.Should().Be(page);
            pagedList.TotalPages.Should().Be(3);
            pagedList.TotalCount.Should().Be(count);
            pagedList.PageSize.Should().Be(size);
        }
    }
}
