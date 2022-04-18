using ApiAlunos.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiAlunos.Infrastructure.Models;

namespace ApiAlunos.Application.Repositories.AlunosRepository
{
    public interface IAlunosRepository : IRepository<Alunos>
    {
        public Task<IEnumerable<Alunos>> GetAll(string name = null);
        public Task DeleteAll();
    }
}
