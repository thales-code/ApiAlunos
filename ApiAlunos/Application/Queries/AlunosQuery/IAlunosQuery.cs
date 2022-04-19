using ApiAlunos.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAlunos.Application.Queries.AlunosQuery
{
    public interface IAlunosQuery
    {
        Task<IEnumerable<Alunos>> ListAllAsync(string nome = null);
        Task<Alunos> ListByIdAsync(int Id);
    }
}
