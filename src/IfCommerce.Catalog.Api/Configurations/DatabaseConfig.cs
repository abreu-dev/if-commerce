using IfCommerce.Catalog.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IfCommerce.Catalog.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            
            services.AddDbContext<CatalogContext>(options => options.UseSqlite("Data Source=Catalog.db"));
        }
    }
}
