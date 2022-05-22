using IfCommerce.Catalog.Domain.Entities;
using IfCommerce.Catalog.Infra.Data.Mappings;
using IfCommerce.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IfCommerce.Catalog.Infra.Data.Context
{
    public class CatalogContext : DbContext, IUnitOfWork
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMapping());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            return await SaveChangesAsync() > 0;
        }
    }
}
