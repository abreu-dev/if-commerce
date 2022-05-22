using IfCommerce.Catalog.Infra.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IfCommerce.Catalog.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
