using FluentAssertions;
using IfCommerce.Catalog.Domain.Commands.ProductCommands;
using IfCommerce.Catalog.Domain.Entities;
using System;
using Xunit;

namespace IfCommerce.Catalog.Domain.Tests.Commands
{
    public class ProductCommandTests
    {
        #region AddProductCommand
        [Fact]
        public void AddProductCommand_ShouldBeInvalid_WhenNameNotInformed()
        {
            // Arrange
            var command = new AddProductCommand()
            {
                Product = new Product()
                {
                    Name = string.Empty
                }
            };

            // Act
            command.IsValid();

            // Assert
            command.ValidationResult.IsValid.Should().BeFalse();
            command.ValidationResult.Errors.Should().HaveCount(1);
            command.ValidationResult.Errors.Should().Contain(x => x.ErrorCode == "MissingValue"
                && x.CustomState.ToString() == "Name not informed" && x.ErrorMessage == "The field 'Name' must be informed");
        }

        [Fact]
        public void AddProductCommand_ShouldBeValid()
        {
            // Arrange
            var command = new AddProductCommand()
            {
                Product = new Product()
                {
                    Name = "Name"
                }
            };

            // Act
            command.IsValid();

            // Assert
            command.ValidationResult.IsValid.Should().BeTrue();
        }
        #endregion

        #region UpdateProductCommand
        [Fact]
        public void UpdateProductCommand_ShouldBeInvalid_WhenIdNotInformed()
        {
            // Arrange
            var command = new UpdateProductCommand(Guid.Empty)
            {
                Product = new Product()
                {
                    Name = "Name"
                }
            };

            // Act
            command.IsValid();

            // Assert
            command.ValidationResult.IsValid.Should().BeFalse();
            command.ValidationResult.Errors.Should().HaveCount(1);
            command.ValidationResult.Errors.Should().Contain(x => x.ErrorCode == "MissingValue"
                && x.CustomState.ToString() == "Id not informed" && x.ErrorMessage == "The field 'Id' must be informed");
        }

        [Fact]
        public void UpdateProductCommand_ShouldBeInvalid_WhenNameNotInformed()
        {
            // Arrange
            var command = new UpdateProductCommand(Guid.NewGuid())
            {
                Product = new Product()
                {
                    Name = string.Empty
                }
            };

            // Act
            command.IsValid();

            // Assert
            command.ValidationResult.IsValid.Should().BeFalse();
            command.ValidationResult.Errors.Should().HaveCount(1);
            command.ValidationResult.Errors.Should().Contain(x => x.ErrorCode == "MissingValue"
                && x.CustomState.ToString() == "Name not informed" && x.ErrorMessage == "The field 'Name' must be informed");
        }

        [Fact]
        public void UpdateProductCommand_ShouldBeValid()
        {
            // Arrange
            var command = new UpdateProductCommand(Guid.NewGuid())
            {
                Product = new Product()
                {
                    Name = "Name"
                }
            };

            // Act
            command.IsValid();

            // Assert
            command.ValidationResult.IsValid.Should().BeTrue();
        }
        #endregion

        #region DeleteProductCommand
        [Fact]
        public void DeleteProductCommand_ShouldBeInvalid_WhenIdNotInformed()
        {
            // Arrange
            var command = new DeleteProductCommand(Guid.Empty);

            // Act
            command.IsValid();

            // Assert
            command.ValidationResult.IsValid.Should().BeFalse();
            command.ValidationResult.Errors.Should().HaveCount(1);
            command.ValidationResult.Errors.Should().Contain(x => x.ErrorCode == "MissingValue"
                && x.CustomState.ToString() == "Id not informed" && x.ErrorMessage == "The field 'Id' must be informed");
        }

        [Fact]
        public void DeleteProductCommand_ShouldBeValid()
        {
            // Arrange
            var command = new DeleteProductCommand(Guid.NewGuid());

            // Act
            command.IsValid();

            // Assert
            command.ValidationResult.IsValid.Should().BeTrue();
        }
        #endregion
    }
}
