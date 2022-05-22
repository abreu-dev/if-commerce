using IfCommerce.Core.Mediator;
using IfCommerce.Core.Messaging;
using IfCommerce.Core.Messaging.Notifications;
using MediatR;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace IfCommerce.Core.Tests.Mediator
{
    public class MediatorHandlerTests
    {
        public class ConcreteCommand : Command
        {
            public ConcreteCommand(Guid aggregateId) : base(aggregateId) { }

            public override bool IsValid()
            {
                return true;
            }
        }

        public class ConcreteEvent : Event { }

        private readonly Mock<IMediator> _mediator;
        private readonly IMediatorHandler _mediatorHandler;

        public MediatorHandlerTests()
        {
            _mediator = new Mock<IMediator>();
            _mediatorHandler = new MediatorHandler(_mediator.Object);
        }

        [Fact]
        public void SendCommand_ShouldCallMediatorSend()
        {
            // Arrange
            var command = new ConcreteCommand(Guid.NewGuid());

            // Act
            _mediatorHandler.SendCommand(command).Wait();

            // Assert
            _mediator.Verify(mock => mock.Send(command, CancellationToken.None), Times.Once);
        }

        [Fact]
        public void PublishEvent_ShouldCallMediatorPublish()
        {
            // Arrange
            var @event = new ConcreteEvent();

            // Act
            _mediatorHandler.PublishEvent(@event).Wait();

            // Assert
            _mediator.Verify(mock => mock.Publish(@event, CancellationToken.None), Times.Once);
        }

        [Fact]
        public void PublishDomainNotification_ShouldCallMediatorPublish()
        {
            // Arrange
            var notification = new DomainNotification("", "", "");

            // Act
            _mediatorHandler.PublishDomainNotification(notification).Wait();

            // Assert
            _mediator.Verify(mock => mock.Publish(notification, CancellationToken.None), Times.Once);
        }
    }
}
