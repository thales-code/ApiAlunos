using ApiAlunos.Application.Services.DbSession;
using System.Data;

namespace ApiAlunos.Application.Services.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        public IDbSession _session { get; private set; }

        public UnitOfWork(IDbSession session)
        {
            _session = session;
        }

        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _session.Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();
        }

        public void Dispose() => _session.Transaction?.Dispose();
    }
}
