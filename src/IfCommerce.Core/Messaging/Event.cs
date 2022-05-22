using MediatR;

namespace IfCommerce.Core.Messaging
{
    public abstract class Event : Message, INotification { }
}
