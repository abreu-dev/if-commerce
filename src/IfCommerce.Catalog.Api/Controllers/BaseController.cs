using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IfCommerce.Catalog.Api.Controllers
{
    [ApiController]
    [Route("catalog/")]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult ServiceUnavailable()
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, null);
        }
    }
}
