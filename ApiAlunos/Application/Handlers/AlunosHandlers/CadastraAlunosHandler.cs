using ApiAlunos.Application.Commands.AlunosCommands;
using ApiAlunos.Application.Repositories.AlunosRepository;
using ApiAlunos.Application.Services.UnitOfWork;
using ApiAlunos.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiAlunos.Application.Handlers.AlunosHandlers
{
    public class CadastraAlunosHandler : IRequestHandler<CadastraAlunosCommand, Alunos>
    {
        private readonly IAlunosRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public CadastraAlunosHandler(IAlunosRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Alunos> Handle(CadastraAlunosCommand request, CancellationToken cancellationToken)
        {
            var alunos = new Alunos { Nome = request.Nome, DataNascimento = request.DataNascimento, Cidade = request.Cidade, Estado = request.Estado, DataMatricula = DateTime.UtcNow };

            try
            {
                _unitOfWork.BeginTransaction();
                var alunoId = await _repository.Add(alunos);
                var aluno = await _repository.GetById(alunoId);
                _unitOfWork.Commit();

                return aluno;
            }
            catch
            {
                _unitOfWork.Rollback();
                throw new Exception("Ocorreu um erro no momento da criação");
            }

        }
    }
}
