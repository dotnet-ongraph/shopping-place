using Microsoft.EntityFrameworkCore;
using Catalog.Core.Entities;
using Infrastructure;
using Core.Interfaces;

namespace Catalog.Infrastructure
{
    public class CatalogDbContext : EntityContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> dbContextOptions, IReadEntityRepository readEntityRepository,
                                    IWriteEntityRepository writeEntityRepository) : base(dbContextOptions, readEntityRepository, writeEntityRepository)
        {   
            this.Name = ContextName.ConnStr;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("catalog");
            base.OnModelCreating(modelBuilder);
        }
    }
}