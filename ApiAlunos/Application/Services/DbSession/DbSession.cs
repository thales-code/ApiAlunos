using System.Data;

namespace ApiAlunos.Application.Services.DbSession
{
    public sealed class DbSession : IDbSession
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession(IDbConnection connection)
        {
            Connection = connection;

            if (!connection.State.HasFlag(ConnectionState.Open))
            {
                Connection.Open();
            };
        }

        public void Dispose() => Connection?.Dispose();
    }
}