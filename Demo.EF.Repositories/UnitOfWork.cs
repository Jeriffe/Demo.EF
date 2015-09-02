using Demo.EF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.EF.Repositories
{
    public class UnitOfWork : IUnitOfWork<DbContext>
    {
        private bool isDisposed;
        private DbContextTransaction transaction = null;
        public DbContext Context
        {
            get; private set;
        }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }
      
        public void BeginTransaction()
        {
            if (transaction != null)
            {
                throw new Exception("CANNOT_BEGIN_NEW_TRANSACTION_WHILE_A_TRANSACTION_IS_RUNNING");
            }

            transaction = Context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void CommitTransaction()
        {
            if (transaction == null)
            {
                throw new Exception("CANNOT_COMMIT_TRANSACTION_WHILE_NO_TRANSACTION_IS_RUNNING");
            }

            transaction.Commit();

            ReleaseTransaction();
        }

        public void RollBackTransaction()
        {
            if (transaction == null)
            {
                throw new Exception("CANNOT_ROLLBACK_TRANSACTION_WHILE_NO_TRANSACTION_IS_RUNNING");
            }

            transaction.Rollback();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return Context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return Context.Database.SqlQuery<TElement>(sql, parameters);
        }

        private void ReleaseTransaction()
        {
            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }

            CloseConnection();
        }

        private void CloseConnection()
        {
            if (Context.Database == null || Context.Database.Connection == null)
            {
                return;
            }

            if (Context.Database.Connection.State == ConnectionState.Open)
            {
                Context.Database.Connection.Close();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    if (Context != null)
                    {
                        Context.Dispose();
                        Context = null;
                    }
                }

                isDisposed = true;
            }
        }
      
    }
}
