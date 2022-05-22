using IfCommerce.Catalog.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IfCommerce.Catalog.Api.Configurations
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToContractMappingProfile), typeof(ContractToDomainMappingProfile));
        }
    }
}
