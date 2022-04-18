using ApiAlunos.Application.Commands.HostedServicesCommands;
using ApiAlunos.Application.HostedServices.AddAluno;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiAlunos.Application.Handlers.HostedServicesHandlers
{
    public class AtualizaTimerHandler : IRequestHandler<AtualizaTimerCommand, object>
    {
        public Task<object> Handle(AtualizaTimerCommand request, CancellationToken cancellationToken)
        {
            AddAlunoTimer.Minutes = request.Minutes;


            return Task.FromResult((object)new
            {
                AddAlunoTimer.Minutes
            });
        }
    }
}
