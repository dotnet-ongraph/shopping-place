using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IEntityRepository<T> : IDisposable where T : BaseEntity
    {
        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        void SaveChanges();

        T Get(long id);

        IEnumerable<T> GetAll();

        IQueryable<T> Find(Expression<Func<T, bool>> expression, List<string> includeProperties = null);
    }
}
