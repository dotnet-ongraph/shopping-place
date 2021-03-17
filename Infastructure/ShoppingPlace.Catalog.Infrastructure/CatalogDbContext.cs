using Microsoft.EntityFrameworkCore;
using Catalog.Core.Entities;
using Infrastructure;

namespace Catalog.Infrastructure
{
    public class CatalogDbContext : EntityContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Catalog.Core.Entities.Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("product");
            base.OnModelCreating(modelBuilder);
        }
    }
}