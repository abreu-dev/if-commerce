using IfCommerce.Catalog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IfCommerce.Catalog.Api.Controllers
{
    public class HealthController : BaseController
    {
        private readonly IHealthService _healthService;

        public HealthController(IHealthService healthService)
        {
            _healthService = healthService;
        }

        [HttpGet("[controller]")]
        public IActionResult Get()
        {
            if (_healthService.IsHealthy())
            {
                return Ok();
            }

            return ServiceUnavailable();
        }
    }
}
