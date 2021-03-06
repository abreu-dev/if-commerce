using IfCommerce.Catalog.Domain.Entities;
using IfCommerce.Catalog.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IfCommerce.Catalog.Infra.Data.Context
{
    public class CatalogContext : DbContext, ICatalogContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMapping());
            modelBuilder.ApplyConfiguration(new CategoryMapping());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            return await SaveChangesAsync() > 0;
        }

        public bool IsAvailable()
        {
            return Database.CanConnect();
        }
    }
}
