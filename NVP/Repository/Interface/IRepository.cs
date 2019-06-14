using System;
using System.Linq;
using System.Linq.Expressions;

namespace NVP.Repository.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(long id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Remove(TEntity entity);
    }
}
