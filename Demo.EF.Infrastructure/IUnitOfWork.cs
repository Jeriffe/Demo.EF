using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.EF.Infrastructure
{
    public interface IUnitOfWork:IDisposable
    {
        void BeginTransaction();

        void RollBackTransaction();

        void CommitTransaction();
    }

    public interface IUnitOfWork<out TContext> : IUnitOfWork
    {
        TContext Context { get; }
    }

}
