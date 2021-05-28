using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Fulfillment.Core.Entities;
using Core.Interfaces;

namespace Fulfillment.Infrastructure
{
    public class FulfillmentDbContext : EntityContext
    {
        public FulfillmentDbContext(DbContextOptions<FulfillmentDbContext> dbContextOptions, IReadEntityRepository readEntityRepository,
                                    IWriteEntityRepository writeEntityRepository) : base(dbContextOptions, readEntityRepository, writeEntityRepository)
        {
            this.Name = ContextName.FulfillmentDb;
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("fulfillment");
            base.OnModelCreating(modelBuilder);
        }
    }
}