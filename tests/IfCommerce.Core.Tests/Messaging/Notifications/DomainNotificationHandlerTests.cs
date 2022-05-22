using FluentAssertions;
using IfCommerce.Core.Messaging.Notifications;
using System.Threading;
using Xunit;

namespace IfCommerce.Core.Tests.Messaging.Notifications
{
    public class DomainNotificationHandlerTests
    {
        private readonly DomainNotificationHandler _domainNotificationHandler;

        public DomainNotificationHandlerTests()
        {
            _domainNotificationHandler = new DomainNotificationHandler();
        }

        [Fact]
        public void Handle_ShouldAddDomainNotification()
        {
            // Arrange
            var notification = new DomainNotification("", "", "");

            // Act
            _domainNotificationHandler.Handle(notification, CancellationToken.None);

            // Assert
            _domainNotificationHandler.GetNotifications().Should().Contain(notification);
        }

        [Fact]
        public void GetNotifications_ShouldReturnDomainNotificationList()
        {
            // Arrange & Act
            var notifications = _domainNotificationHandler.GetNotifications();

            // Assert
            notifications.Should().BeEmpty();
        }

        [Fact]
        public void HasNotifications_ShouldReturnFalse_WhenEmptyNotifications()
        {
            // Arrange & Act
            var notifications = _domainNotificationHandler.HasNotifications();

            // Assert
            notifications.Should().BeFalse();
        }

        [Fact]
        public void HasNotifications_ShouldReturnTrue_WhenHaveNotifications()
        {
            // Arrange
            var notification = new DomainNotification("", "", "");
            _domainNotificationHandler.Handle(notification, CancellationToken.None);

            // Act
            var notifications = _domainNotificationHandler.HasNotifications();

            // Assert
            notifications.Should().BeTrue();
        }

        [Fact]
        public void Dispose_ShouldClearNotifications()
        {
            // Arrange
            var notification = new DomainNotification("", "", "");
            _domainNotificationHandler.Handle(notification, CancellationToken.None);

            // Act
            _domainNotificationHandler.Dispose();

            // Assert
            _domainNotificationHandler.GetNotifications().Should().BeEmpty();
        }
    }
}
