using MediatR;

namespace IfCommerce.Core.Messaging.Notifications
{
    public class DomainNotification : Message, INotification
    {
        public string Type { get; private set; }
        public string Message { get; private set; }
        public string Detail { get; private set; }

        public DomainNotification(string type, string message, string detail)
        {
            Type = type;
            Message = message;
            Detail = detail;
        }
    }
}
