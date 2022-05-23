using FluentAssertions;
using IfCommerce.Core.Domain;
using IfCommerce.Core.Query;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void ToPagedList_ShouldReturnPageListForFirstPage()
        {
            // Arrange
            var source = new List<ConcreteEntity>()
            {
                new ConcreteEntity() { Id = Guid.NewGuid() },
                new ConcreteEntity() { Id = Guid.NewGuid() },
                new ConcreteEntity() { Id = Guid.NewGuid() },
                new ConcreteEntity() { Id = Guid.NewGuid() },
                new ConcreteEntity() { Id = Guid.NewGuid() },
                new ConcreteEntity() { Id = Guid.NewGuid() }
            };
            var page = 1;
            var size = 2;

            // Act
            var pagedList = PagedList<ConcreteEntity>.ToPagedList(source.AsQueryable(), page, size);

            // Assert
            pagedList.Data.Should().BeEquivalentTo(source.Take(size));
            pagedList.CurrentPage.Should().Be(page);
            pagedList.PageSize.Should().Be(size);
            pagedList.TotalPages.Should().Be(3);
            pagedList.TotalCount.Should().Be(6);
            pagedList.HasPrevious.Should().BeFalse();
            pagedList.HasNext.Should().BeTrue();
        }

        [Fact]
        public void ToPagedList_ShouldReturnPageListForMiddlePage()
        {
            // Arrange
            var source = new List<ConcreteEntity>()
            {
                new ConcreteEntity() { Id = Guid.NewGuid() },
                new ConcreteEntity() { Id = Guid.NewGuid() },
                new ConcreteEntity() { Id = Guid.NewGuid() },
                new ConcreteEntity() { Id = Guid.NewGuid() },
                new ConcreteEntity() { Id = Guid.NewGuid() },
                new ConcreteEntity() { Id = Guid.NewGuid() }
            };
            var page = 2;
            var size = 2;

            // Act
            var pagedList = PagedList<ConcreteEntity>.ToPagedList(source.AsQueryable(), page, size);

            // Assert
            pagedList.Data.Should().BeEquivalentTo(source.Skip(2).Take(size));
            pagedList.CurrentPage.Should().Be(page);
            pagedList.PageSize.Should().Be(size);
            pagedList.TotalPages.Should().Be(3);
            pagedList.TotalCount.Should().Be(6);
            pagedList.HasPrevious.Should().BeTrue();
            pagedList.HasNext.Should().BeTrue();
        }

        [Fact]
        public void ToPagedList_ShouldReturnPageListForLastPage()
        {
            // Arrange
            var source = new List<ConcreteEntity>()
            {
                new ConcreteEntity() { Id = Guid.NewGuid() },
                new ConcreteEntity() { Id = Guid.NewGuid() },
                new ConcreteEntity() { Id = Guid.NewGuid() },
                new ConcreteEntity() { Id = Guid.NewGuid() },
                new ConcreteEntity() { Id = Guid.NewGuid() },
                new ConcreteEntity() { Id = Guid.NewGuid() }
            };
            var page = 3;
            var size = 2;

            // Act
            var pagedList = PagedList<ConcreteEntity>.ToPagedList(source.AsQueryable(), page, size);

            // Assert
            pagedList.Data.Should().BeEquivalentTo(source.Skip(4).Take(size));
            pagedList.CurrentPage.Should().Be(page);
            pagedList.PageSize.Should().Be(size);
            pagedList.TotalPages.Should().Be(3);
            pagedList.TotalCount.Should().Be(6);
            pagedList.HasPrevious.Should().BeTrue();
            pagedList.HasNext.Should().BeFalse();
        }
    }
}
