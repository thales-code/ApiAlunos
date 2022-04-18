using ApiAlunos.Application.Services.DbSession;
using ApiAlunos.Domain.Entities;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAlunos.Application.Repositories.AlunosRepository
{
    public class AlunosRepository : IAlunosRepository
    {
        private IDbSession _session;

        public AlunosRepository(IDbSession session)
        {
            _session = session;
        }

        public Task<IEnumerable<Alunos>> GetAll(string nome)
        {
            using var connection = _session.Connection;
            string sql = "select * from alunos ";


            if (!string.IsNullOrEmpty(nome))
            {
                sql += "where nome like (@nome)";
            }


            return connection.QueryAsync<Alunos>(sql, new { nome = $"%{nome}%" }, _session.Transaction);
        }

        public Task<Alunos> GetById(int id)
        {
            using var connection = _session.Connection;
            string sql = "select * from alunos where id = @id";
            return connection.QueryFirstOrDefaultAsync<Alunos>(sql, new { id }, _session.Transaction);
        }

        public Task<int> Add(Alunos Alunos)
        {
            string sql = @"insert into alunos(nome, datanascimento, cidade, estado, datamatricula) values (@nome, @datanascimento, @cidade, @estado, @datamatricula);
                            select LAST_INSERT_ID()";
            return _session.Connection.ExecuteScalarAsync<int>(sql, Alunos, _session.Transaction);
        }

        public Task Edit(Alunos Alunos)
        {
            string sql = @"update alunos set nome = @nome, datanascimento = @datanascimento, cidade = @cidade, estado = @estado, datamatricula = @datamatricula where id = @id;";
            return _session.Connection.ExecuteAsync(sql, Alunos, _session.Transaction);
        }

        public Task Delete(int id)
        {
            string sql = "delete from alunos where id = @id";
            return _session.Connection.ExecuteAsync(sql, new { id }, _session.Transaction);
        }

        public Task DeleteAll()
        {
            string sql = "truncate alunos";
            return _session.Connection.ExecuteAsync(sql, null, _session.Transaction);
        }

        public Task<IEnumerable<Alunos>> GetAll()
        {
            return GetAll(null);
        }
    }
}
