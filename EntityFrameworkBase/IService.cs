using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkBase
{
    public interface IService<TEntity, Tkey> where TEntity : class
    {
        IEnumerable<TEntity> Get();

        TEntity Get(Tkey id);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
