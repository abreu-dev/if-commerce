using IfCommerce.Core.Mediator;
using IfCommerce.Core.Messaging.Notifications;
using System.Threading.Tasks;

namespace IfCommerce.Core.Messaging
{
    public abstract class CommandHandler
    {
        protected readonly IMediatorHandler _mediatorHandler;

        protected CommandHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task PublishValidationErrors(Command request)
        {
            foreach (var error in request.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishDomainNotification(
                    new DomainNotification(error.ErrorCode, error.CustomState.ToString(), error.ErrorMessage));
            }
        }
    }
}
