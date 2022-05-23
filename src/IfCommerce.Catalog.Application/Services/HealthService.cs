using IfCommerce.Catalog.Application.Interfaces;
using IfCommerce.Catalog.Infra.Data.Context;

namespace IfCommerce.Catalog.Application.Services
{
    public class HealthService : IHealthService
    {
        private readonly ICatalogContext _catalogContext;

        public HealthService(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public bool IsHealthy()
        {
            return _catalogContext.IsAvailable();
        }
    }
}
