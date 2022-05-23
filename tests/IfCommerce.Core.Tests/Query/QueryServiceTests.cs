using FluentAssertions;
using IfCommerce.Core.Domain;
using IfCommerce.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IfCommerce.Core.Tests.Query
{
    public class QueryServiceTests
    {
        public class ConcreteEntity : Entity 
        {
            public int Value { get; set; }
            public string Name { get; set; }
        }

        private readonly IQueryService _queryService;

        public QueryServiceTests()
        {
            _queryService = new QueryService();
        }

        [Fact]
        public void Ordering_ShouldBeInAscendingOrder()
        {
            // Arrange
            var source = new List<ConcreteEntity>()
            {
                new ConcreteEntity() { Id = Guid.NewGuid(), Value = 1 },
                new ConcreteEntity() { Id = Guid.NewGuid(), Value = 2 },
                new ConcreteEntity() { Id = Guid.NewGuid(), Value = 3 }
            };

            // Act
            var ordered = _queryService.Ordering(source.AsQueryable(), "Value");

            // Assert
            ordered.Should().BeInAscendingOrder(x => x.Value);
        }

        [Fact]
        public void Ordering_ShouldBeInDescendingOrder()
        {
            // Arrange
            var source = new List<ConcreteEntity>()
            {
                new ConcreteEntity() { Id = Guid.NewGuid(), Value = 1 },
                new ConcreteEntity() { Id = Guid.NewGuid(), Value = 2 },
                new ConcreteEntity() { Id = Guid.NewGuid(), Value = 3 }
            };

            // Act
            var ordered = _queryService.Ordering(source.AsQueryable(), "Value desc");

            // Assert
            ordered.Should().BeInDescendingOrder(x => x.Value);
        }

        [Fact]
        public void Filtering_ShouldFilterByString_UsingEqualStatement()
        {
            // Arrange
            var armadillo = new ConcreteEntity() { Id = Guid.NewGuid(), Name = "Armadillo" };
            var lizard = new ConcreteEntity() { Id = Guid.NewGuid(), Name = "Lizard" };
            var armadilloLizard = new ConcreteEntity() { Id = Guid.NewGuid(), Name = "Armadillo Lizard" };

            var source = new List<ConcreteEntity>()
            {
                armadillo, lizard, armadilloLizard
            };

            // Act
            var filtered = _queryService.Filtering(source.AsQueryable(), "Name", new List<string>() { "Armadillo" });

            // Assert
            filtered.Should().HaveCount(1);
            filtered.Should().Contain(armadillo);
        }

        [Theory]
        [InlineData("Armadillo*")]
        [InlineData("armadillo*")]
        [InlineData("ARMADILLO*")]
        public void Filtering_ShouldFilterByString_UsingStartsWithStatement(string filter)
        {
            // Arrange
            var armadillo = new ConcreteEntity() { Id = Guid.NewGuid(), Name = "Armadillo" };
            var lizard = new ConcreteEntity() { Id = Guid.NewGuid(), Name = "Lizard" };
            var armadilloLizard = new ConcreteEntity() { Id = Guid.NewGuid(), Name = "Armadillo Lizard" };

            var source = new List<ConcreteEntity>()
            {
                armadillo, lizard, armadilloLizard
            };

            // Act
            var filtered = _queryService.Filtering(source.AsQueryable(), "Name", new List<string>() { filter });

            // Assert
            filtered.Should().HaveCount(2);
            filtered.Should().Contain(armadillo);
            filtered.Should().Contain(armadilloLizard);
        }

        [Theory]
        [InlineData("*Lizard")]
        [InlineData("*lizard")]
        [InlineData("*LIZARD")]
        public void Filtering_ShouldFilterByString_UsingEndsWithStatement(string filter)
        {
            // Arrange
            var armadillo = new ConcreteEntity() { Id = Guid.NewGuid(), Name = "Armadillo" };
            var lizard = new ConcreteEntity() { Id = Guid.NewGuid(), Name = "Lizard" };
            var armadilloLizard = new ConcreteEntity() { Id = Guid.NewGuid(), Name = "Armadillo Lizard" };

            var source = new List<ConcreteEntity>()
            {
                armadillo, lizard, armadilloLizard
            };

            // Act
            var filtered = _queryService.Filtering(source.AsQueryable(), "Name", new List<string>() { filter });

            // Assert
            filtered.Should().HaveCount(2);
            filtered.Should().Contain(lizard);
            filtered.Should().Contain(armadilloLizard);
        }

        [Theory]
        [InlineData("*D*")]
        [InlineData("*d*")]
        public void Filtering_ShouldFilterByString_UsingContainsStatement(string filter)
        {
            // Arrange
            var armadillo = new ConcreteEntity() { Id = Guid.NewGuid(), Name = "Armadillo" };
            var lizard = new ConcreteEntity() { Id = Guid.NewGuid(), Name = "Lizard" };
            var armadilloLizard = new ConcreteEntity() { Id = Guid.NewGuid(), Name = "Armadillo Lizard" };

            var source = new List<ConcreteEntity>()
            {
                armadillo, lizard, armadilloLizard
            };

            // Act
            var filtered = _queryService.Filtering(source.AsQueryable(), "Name", new List<string>() { filter });

            // Assert
            filtered.Should().HaveCount(3);
            filtered.Should().Contain(armadillo);
            filtered.Should().Contain(lizard);
            filtered.Should().Contain(armadilloLizard);
        }

        [Fact]
        public void Filtering_ShouldFilterByString_UsingOrStatement()
        {
            // Arrange
            var armadillo = new ConcreteEntity() { Id = Guid.NewGuid(), Name = "Armadillo" };
            var lizard = new ConcreteEntity() { Id = Guid.NewGuid(), Name = "Lizard" };
            var armadilloLizard = new ConcreteEntity() { Id = Guid.NewGuid(), Name = "Armadillo Lizard" };

            var source = new List<ConcreteEntity>()
            {
                armadillo, lizard, armadilloLizard
            };

            // Act
            var filtered = _queryService.Filtering(source.AsQueryable(), "Name", new List<string>() { "Armadillo", "Lizard" });

            // Assert
            filtered.Should().HaveCount(2);
            filtered.Should().Contain(armadillo);
            filtered.Should().Contain(lizard);
        }

        [Fact]
        public void Pagination_ShouldReturnPageListForFirstPage()
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
            var pagedList = _queryService.Pagination(source.AsQueryable(), page, size);

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
        public void Pagination_ShouldReturnPageListForMiddlePage()
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
            var pagedList = _queryService.Pagination(source.AsQueryable(), page, size);

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
        public void Pagination_ShouldReturnPageListForLastPage()
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
            var pagedList = _queryService.Pagination(source.AsQueryable(), page, size);

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
