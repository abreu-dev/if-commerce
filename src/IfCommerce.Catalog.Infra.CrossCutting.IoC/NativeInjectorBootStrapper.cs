using IfCommerce.Core.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace IfCommerce.Catalog.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();
        }
    }
}
