using ApiAlunos.Application.Repositories.AlunosRepository;
using ApiAlunos.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAlunos.Application.Queries.AlunosQuery
{
    public class AlunosQuery : IAlunosQuery
    {
        private readonly IAlunosRepository _alunosRepository;

        public AlunosQuery(IAlunosRepository alunosRepository)
        {
            _alunosRepository = alunosRepository;
        }

        public Task<IEnumerable<Alunos>> ListAllAsync(string nome = null)
        {
            return _alunosRepository.GetAll(nome);
        }

        public Task<Alunos> ListByIdAsync(int Id)
        {
            return _alunosRepository.GetById(Id);
        }
    }
}
