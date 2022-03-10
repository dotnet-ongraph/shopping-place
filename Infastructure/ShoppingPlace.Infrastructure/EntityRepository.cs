using Microsoft.EntityFrameworkCore;
using Core.Base;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure
{
    public class EntityRepository<TEntity, TContext> : IEntityRepository<TEntity> 
                                where TContext : DbContext, IEntityContext 
                                where TEntity : BaseEntity
    {
        private readonly DbContext _context;

        public EntityRepository(TContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> All
        {
            get
            {
                return _context.Set<TEntity>().Where(i => i.IsDeleted == false);
            }
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Remove(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public TEntity Get(long id)
        {
            return All.SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return All.AsEnumerable();
        }

        public void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            
            _context.Add(entity);
            //SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            SaveChanges();
        }


        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, List<string> includeProperties = null)
        {
            IQueryable<TEntity> query = null;
            foreach (var property in includeProperties)
            {
                if (property.Length == 0)
                    continue;
                if (query == null)
                    query = _context.Set<TEntity>().Where(expression).Include(property);
                else
                    query = query.Include(property);
            }
            return query.Where(i => i.IsDeleted == false);
        }

        public void Insert(List<TEntity> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _context.Add(entity);
        }
    }
}