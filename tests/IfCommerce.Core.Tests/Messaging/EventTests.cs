using FluentAssertions;
using IfCommerce.Core.Messaging;
using System;
using Xunit;

namespace IfCommerce.Core.Tests.Messaging
{
    public class EventTests
    {
        public class ConcreteEvent : Event { }

        [Fact]
        public void Constructor_ShouldCreateInstance()
        {
            // Arrange & Act
            var @event = new ConcreteEvent();

            // Assert
            @event.MessageType.Should().Be(typeof(ConcreteEvent).Name);
            @event.AggregateId.Should().Be(Guid.Empty);
            @event.Timestamp.Should().NotBe(default);
        }
    }
}
