using Microsoft.Extensions.DependencyInjection;
using System;

namespace IfCommerce.Catalog.Api.Configurations
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
        }
    }
}
