using IfCommerce.Catalog.Api.Responses;
using IfCommerce.Core.Messaging.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IfCommerce.Catalog.Api.Controllers
{
    [ApiController]
    [Route("catalog")]
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

        protected IActionResult CustomResponse()
        {
            if (_notifications.HasNotifications())
            {
                var instance = HttpContext.Request.Path;
                var traceId = HttpContext.TraceIdentifier;
                var response = new Response(instance, traceId);

                _notifications.GetNotifications().ForEach(notification =>
                {
                    response.Errors.Add(new ResponseError(notification.Type, notification.Message, notification.Detail));
                });

                return BadRequest(response);
            }

            return Ok();
        }
    }
}
