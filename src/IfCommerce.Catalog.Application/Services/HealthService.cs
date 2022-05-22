using IfCommerce.Catalog.Application.Interfaces;

namespace IfCommerce.Catalog.Application.Services
{
    public class HealthService : IHealthService
    {
        public HealthService() { }

        public bool IsHealthy()
        {
            return true;
        }
    }
}
