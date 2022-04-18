using ApiAlunos.Application.Commands.AlunosCommands;
using ApiAlunos.Application.Repositories.AlunosRepository;
using ApiAlunos.Application.Services.UnitOfWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiAlunos.Application.Handlers.AlunosHandlers
{
    public class ExcluirTodosAlunosHandler : IRequestHandler<ExcluirTodosAlunosCommand, object>
    {
        private readonly IAlunosRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public ExcluirTodosAlunosHandler(IAlunosRepository repository, IUnitOfWork unitOfWork = null)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(ExcluirTodosAlunosCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                await _repository.DeleteAll();


                _unitOfWork.Commit();
                return new();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw new Exception("Ocorreu um erro no momento da exclusão");
            }

        }
    }
}
