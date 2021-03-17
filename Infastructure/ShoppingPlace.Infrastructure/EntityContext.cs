using Microsoft.EntityFrameworkCore;
using Core.Base;
using Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class EntityContext : DbContext, IEntityContext
    {

        public EntityContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<BaseEntity>())
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedDate = DateTime.Now;
                }
                if (entity.State == EntityState.Deleted)
                {
                    entity.Entity.IsDeleted = true;
                }
                if (entity.State == EntityState.Added || entity.State == EntityState.Modified || entity.State == EntityState.Deleted)
                {
                    entity.Entity.LastModified = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}