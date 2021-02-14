using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EntityFrameworkBase
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        string TableName { get; }

        IEnumerable<TEntity> GetAll();

        TEntity Get(TKey id);

        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetList(string sql);

        TEntity Update(TEntity entity);

        List<TEntity> UpdateRange(List<TEntity> entitys);

        void Add(TEntity entity);

        void AddRange(List<TEntity> entitys);

        void Remove(TEntity entity);

        void RemoveRange(List<TEntity> entitys);

        void SaveChange();
    }
}
