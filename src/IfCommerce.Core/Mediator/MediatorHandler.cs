using IfCommerce.Core.Messaging;
using IfCommerce.Core.Messaging.Notifications;
using MediatR;
using System.Threading.Tasks;

namespace IfCommerce.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public Task PublishEvent<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }

        public Task PublishDomainNotification<T>(T notification) where T : DomainNotification
        {
            return _mediator.Publish(notification);
        }

        public async Task PublishValidationErrors(Command command)
        {
            foreach (var error in command.ValidationResult.Errors)
            {
                await PublishDomainNotification(new DomainNotification(error.ErrorCode, error.CustomState.ToString(), error.ErrorMessage));
            }
        }
    }
}
