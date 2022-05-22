using IfCommerce.Catalog.Domain.Entities;
using IfCommerce.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace IfCommerce.Catalog.Infra.Data.Context
{
    public interface ICatalogContext : IContext
    {
        DbSet<Product> Products { get; set; }
    }
}
