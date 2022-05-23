using FluentValidation.Results;
using IfCommerce.Core.Mediator;
using IfCommerce.Core.Messaging;
using IfCommerce.Core.Messaging.Notifications;
using Moq;
using System;
using Xunit;

namespace IfCommerce.Core.Tests.Messaging
{
    public class CommandHandlerTests
    {
        public class ConcreteCommandHandler : CommandHandler
        {
            public ConcreteCommandHandler(IMediatorHandler mediatorHandler) : base(mediatorHandler) { }
        }

        public class ConcreteCommand : Command
        {
            public ConcreteCommand(Guid aggregateId) : base(aggregateId) { }
        }

        private readonly Mock<IMediatorHandler> _mediatorHandler;
        private readonly ConcreteCommandHandler _concreteCommandHandler;

        public CommandHandlerTests()
        {
            _mediatorHandler = new Mock<IMediatorHandler>();
            _concreteCommandHandler = new ConcreteCommandHandler(_mediatorHandler.Object);
        }

        [Fact]
        public void PublishValidationErrors_ShouldPublishCommandValidationErrors()
        {
            // Arrange
            var command = new ConcreteCommand(Guid.NewGuid());

            var error = new ValidationFailure(null, "ErrorMessage")
            {
                ErrorCode = "ErrorCode",
                CustomState = "CustomState"
            };
            command.ValidationResult.Errors.Add(error);

            // Act
            _concreteCommandHandler.PublishValidationErrors(command).Wait();

            // Assert
            _mediatorHandler.Verify(mock =>
                mock.PublishDomainNotification(It.Is<DomainNotification>(x => x.Type == error.ErrorCode
                    && x.Message == error.CustomState.ToString() && x.Detail == error.ErrorMessage)),
                Times.Once);
        }
    }
}
