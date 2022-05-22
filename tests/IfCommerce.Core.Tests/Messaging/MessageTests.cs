using FluentAssertions;
using IfCommerce.Core.Messaging;
using System;
using Xunit;

namespace IfCommerce.Core.Tests.Messaging
{
    public class MessageTests
    {
        public class ConcreteMessage : Message { }

        [Fact]
        public void Constructor_ShouldCreateInstance()
        {
            // Arrange & Act
            var message = new ConcreteMessage();

            // Assert
            message.MessageType.Should().Be(typeof(ConcreteMessage).Name);
            message.AggregateId.Should().Be(Guid.Empty);
            message.Timestamp.Should().NotBe(default);
        }
    }
}
