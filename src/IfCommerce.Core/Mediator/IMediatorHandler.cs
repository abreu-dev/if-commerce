using IfCommerce.Core.Messaging;
using IfCommerce.Core.Messaging.Notifications;
using System.Threading.Tasks;

namespace IfCommerce.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task PublishEvent<T>(T @event) where T : Event;
        Task PublishDomainNotification<T>(T notification) where T : DomainNotification;
    }
}
