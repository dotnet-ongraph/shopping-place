using Microsoft.EntityFrameworkCore;
using Infrastructure;

namespace Fulfillment.Infrastructure
{
    public class OrderDbContext : EntityContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Fulfillment.Core.Entities.Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("order");
            base.OnModelCreating(modelBuilder);
        }
    }
}