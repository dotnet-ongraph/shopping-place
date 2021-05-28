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

        private readonly IReadEntityRepository _readRepository;
        private readonly IWriteEntityRepository _writeRepository;
        public EntityContext(DbContextOptions dbContextOptions, IReadEntityRepository readRepository, IWriteEntityRepository writeRepository) : base(dbContextOptions)
        {
            this._readRepository = readRepository;
            this._writeRepository = writeRepository;
        }
        public IReadEntityRepository ReadRepository => _readRepository;

        public IWriteEntityRepository WriteRepository => _writeRepository;
        public string _connectionString { get; }

        private ContextName _name;
        public ContextName Name
        {
            get => _name;
            protected set
            {
                _name = value;
                _readRepository.SetReadConnectionString(_name);
                _writeRepository.SetWriteConnectionString(_name);
            }
        }
        public override int SaveChanges()
        {
            SetBaseEntities();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetBaseEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetBaseEntities()
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
        }
    }
}