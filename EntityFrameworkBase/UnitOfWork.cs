using Microsoft.EntityFrameworkCore;
using System;

namespace EntityFrameworkBase
{
    public class UnitOfWork<TContext> : IUnitOfWork, IDisposable where TContext : DbContext
    {
        protected readonly TContext context;
        protected Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction;

        public UnitOfWork(TContext context)
        {
            this.context = context;
        }

        public void Begin()
        {
            transaction = context.Database.BeginTransaction();
        }

        public void Commit()
        {
            transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            if (transaction == null) return;

            transaction.Dispose();
        }

        
    }
}
