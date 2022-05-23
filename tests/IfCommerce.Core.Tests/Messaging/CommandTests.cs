using FluentAssertions;
using IfCommerce.Core.Messaging;
using System;
using Xunit;

namespace IfCommerce.Core.Tests.Messaging
{
    public class CommandTests
    {
        public class ConcreteCommand : Command
        {
            public ConcreteCommand(Guid aggregateId) : base(aggregateId) { }
        }

        [Fact]
        public void Constructor_ShouldCreateInstance()
        {
            // Arrange & Act
            var aggregateId = Guid.NewGuid();
            var command = new ConcreteCommand(aggregateId);

            // Assert
            command.MessageType.Should().Be(typeof(ConcreteCommand).Name);
            command.AggregateId.Should().Be(aggregateId);
            command.Timestamp.Should().NotBe(default);
        }
    }
}
