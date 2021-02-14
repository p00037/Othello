using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkBase
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin();
        void Commit();
        void Rollback();
    }
}
