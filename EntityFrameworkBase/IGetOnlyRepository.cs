using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EntityFrameworkBase
{
    public interface IGetOnlyRepository<TEntity, TKey> where TEntity : class
    {
        TEntity Get(TKey id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetList(string sql);
    }
}
