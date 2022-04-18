using ApiAlunos.Application.Commands.AlunosCommands;
using ApiAlunos.Application.Repositories.AlunosRepository;
using ApiAlunos.Application.Services.UnitOfWork;
using ApiAlunos.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiAlunos.Application.Handlers.AlunosHandlers
{
    public class AtualizaAlunosHandler : IRequestHandler<AtualizaAlunosCommand, Alunos>
    {
        private readonly IAlunosRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AtualizaAlunosHandler(IAlunosRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Alunos> Handle(AtualizaAlunosCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var alunosOriginal = await _repository.GetById(request.Id);

                var alunos = _mapper.Map(request, alunosOriginal);
                await _repository.Edit(alunos);
                _unitOfWork.Commit();

                return alunos;
            }
            catch
            {
                _unitOfWork.Rollback();
                throw new Exception("Ocorreu um erro no momento da alteração");
            }

        }
    }
}
