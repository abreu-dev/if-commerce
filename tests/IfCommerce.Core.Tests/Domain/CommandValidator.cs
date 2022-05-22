using FluentAssertions;
using IfCommerce.Core.Domain;
using IfCommerce.Core.Messaging;
using System;
using System.Linq;
using Xunit;

namespace IfCommerce.Core.Tests.Domain
{
    public class CommandValidator
    {
        public class ConcreteCommand : Command
        {
            public ConcreteCommand(Guid aggregateId) : base(aggregateId) { }
        }

        public class ConcreteCommandValidator : CommandValidator<ConcreteCommand>
        {
            public ConcreteCommandValidator()
            {
                ValidateId();
            }
        }

        [Fact]
        public void Validate_ShouldBeInvalid_WhenAggregateIdNotInformed()
        {
            // Arrange
            var command = new ConcreteCommand(Guid.Empty);
            var validator = new ConcreteCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(1);
            result.Errors.Single().ErrorCode.Should().Be("MissingValue");
            result.Errors.Single().CustomState.Should().Be("Id not informed");
            result.Errors.Single().ErrorMessage.Should().Be("The field 'Id' must be informed");
        }

        [Fact]
        public void Validate_ShouldBeValid_WhenAggregateIdInformed()
        {
            // Arrange
            var command = new ConcreteCommand(Guid.NewGuid());
            var validator = new ConcreteCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
