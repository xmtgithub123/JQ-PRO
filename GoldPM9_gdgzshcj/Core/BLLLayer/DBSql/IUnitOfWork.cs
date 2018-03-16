using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;

namespace DBSql
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsInTransaction { get; }

        void BeginTransaction();

        void BeginTransaction(IsolationLevel isolationLevel);

        void RollBackTransaction();

        void CommitTransaction();

        void SaveChanges();

        void SaveChanges(System.Data.Entity.Core.Objects.SaveOptions saveOptions);
    }
}
