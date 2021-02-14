using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EntityFrameworkBase
{
    public class RepositoryBase<TContext, TEntity, TKey> : IRepository<TEntity, TKey> where TContext : DbContext where TEntity : class
    {
        protected readonly DbSet<TEntity> dbset;

        protected readonly TContext context;

        public RepositoryBase(TContext context)
        {
            this.context = context;
            this.dbset = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbset.AsEnumerable();
        }

        public virtual TEntity Get(TKey id)
        {
            return dbset.Find(id);
        }

        public virtual IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> query = dbset.Where(predicate).AsEnumerable();
            return query;
        }

        public virtual IEnumerable<TEntity> GetList(string where)
        {
            IEnumerable<TEntity> query = dbset.FromSqlRaw(where);
            return query;
        }

        public virtual TEntity Update(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbset.Attach(entity);
            }

            context.Entry(entity).State = EntityState.Modified;
            SaveChange();
            return entity;
        }

        public virtual List<TEntity> UpdateRange(List<TEntity> entitys)
        {
            foreach (var entity in entitys)
            {
                if (context.Entry(entity).State == EntityState.Detached)
                {
                    dbset.Attach(entity);
                }

                context.Entry(entity).State = EntityState.Modified;
            }

            SaveChange();
            return entitys;
        }

        public virtual void Add(TEntity entity)
        {
            dbset.Add(entity);
            SaveChange();
        }

        public virtual void AddRange(List<TEntity> entitys)
        {
            foreach (var entity in entitys)
            {
                dbset.Add(entity);
            }
            SaveChange();
        }

        public virtual void Remove(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbset.Attach(entity);
            }

            dbset.Remove(entity);
            SaveChange();
        }

        public virtual void RemoveRange(List<TEntity> entitys)
        {
            foreach (var entity in entitys)
            {
                if (context.Entry(entity).State == EntityState.Detached)
                {
                    dbset.Attach(entity);
                }

                dbset.Remove(entity);
            }
            
            SaveChange();
        }

        public virtual void SaveChange()
        {
            context.SaveChanges();
        }

        public string TableName
        {
            get
            {
                // Get all the entity types information contained in the DbContext class, ...
                var entityTypes = context.Model.GetEntityTypes();

                // ... and get one by entity type information of "TEntity" DbSet property.
                var entityTypeOfTEntity = entityTypes.First(t => t.ClrType == typeof(TEntity));

                // The entity type information has the actual table name as an annotation!
                var tableNameAnnotation = entityTypeOfTEntity.GetAnnotation("Relational:TableName");
                
                var tableNameOfTEntitySet = tableNameAnnotation.Value.ToString();

                return tableNameOfTEntitySet;
            }
        }
    }
}
