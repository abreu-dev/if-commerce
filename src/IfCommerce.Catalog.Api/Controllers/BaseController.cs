using IfCommerce.Catalog.Api.Responses;
using IfCommerce.Core.Messaging.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IfCommerce.Catalog.Api.Controllers
{
    [ApiController]
    [Route("catalog/")]
    public abstract class BaseController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;

        protected BaseController() { }

        protected BaseController(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected IActionResult ServiceUnavailable()
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, null);
        }

        protected IActionResult BadRequest(string instance)
        {
            var response = new Response(instance);

            _notifications.GetNotifications().ForEach(notification =>
            {
                response.Errors.Add(new ResponseError(notification.Type, notification.Message, notification.Detail));
            });

            return BadRequest(response);
        }

        protected IActionResult CustomResponse(string instance)
        {
            if (_notifications.HasNotifications())
            {
                return BadRequest(instance);
            }

            return Ok();
        }
    }
}
