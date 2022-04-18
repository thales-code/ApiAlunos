using System;
using System.Data;

namespace ApiAlunos.Application.Services.DbSession
{
    public interface IDbSession
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }
    }
}
