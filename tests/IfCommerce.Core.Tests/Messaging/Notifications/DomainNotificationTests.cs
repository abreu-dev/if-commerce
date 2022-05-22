using FluentAssertions;
using IfCommerce.Core.Messaging.Notifications;
using System;
using Xunit;

namespace IfCommerce.Core.Tests.Messaging.Notifications
{
    public class DomainNotificationTests
    {
        [Fact]
        public void Constructor_ShouldCreateInstance()
        {
            // Arrange
            var type = "Type";
            var message = "Message";
            var detail = "Detail";

            // Act
            var notification = new DomainNotification(type, message, detail);

            // Assert
            notification.MessageType.Should().Be(typeof(DomainNotification).Name);
            notification.AggregateId.Should().Be(Guid.Empty);
            notification.Timestamp.Should().NotBe(default);
            notification.Type.Should().Be(type);
            notification.Message.Should().Be(message);
            notification.Detail.Should().Be(detail);
        }
    }
}
