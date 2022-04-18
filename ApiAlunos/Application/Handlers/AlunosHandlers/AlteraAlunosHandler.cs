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
    public class AlteraAlunosHandler : IRequestHandler<AlteraAlunosCommand, Alunos>
    {
        private readonly IAlunosRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public AlteraAlunosHandler(IAlunosRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Alunos> Handle(AlteraAlunosCommand request, CancellationToken cancellationToken)
        {
            var alunos = new Alunos { Id = request.Id, Nome = request.Nome, DataNascimento = request.DataNascimento, Cidade = request.Cidade, Estado = request.Estado, DataMatricula = request.DataMatricula };

            try
            {
                _unitOfWork.BeginTransaction();
                await _repository.Edit(alunos);
                _unitOfWork.Commit();

                return new();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw new Exception("Ocorreu um erro no momento da alteração");
            }

        }
    }
}
