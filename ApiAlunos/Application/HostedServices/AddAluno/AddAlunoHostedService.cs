using ApiAlunos.Application.Commands.AlunosCommands;
using ApiAlunos.Application.HostedServices.AddAluno;
using ApiAlunos.Application.Repositories.AlunosRepository;
using ApiAlunos.Domain.Entities;
using Bogus;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiAlunos.Application.HostedServices.AddAluno
{
    public class AddAlunoHostedService : IHostedService
    {
        private readonly IMediator _mediator;
        private Timer _timer;
        private Task _executingTask;
        private ILogger _logger;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        public AddAlunoHostedService(IMediator mediator, ILogger<AddAlunoHostedService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ExecuteTask, null, TimeSpan.Zero, TimeSpan.FromSeconds(AddAlunoTimer.Minutes * 60));
            return Task.CompletedTask;
        }

        private void ExecuteTask(object state)
        {
            _executingTask = ExecuteTaskAsync(_stoppingCts.Token);
        }

        private async Task ExecuteTaskAsync(CancellationToken stoppingToken)
        {
            _timer.Change(Timeout.Infinite, 0);
            _logger.LogInformation("Inserindo alunos!");
            var taskAlunos = new Faker<CadastraAlunosCommand>()
                .StrictMode(true)
                .RuleFor(x => x.Nome, x => x.Name.FullName())
                .RuleFor(x => x.Cidade, x => x.Address.City())
                .RuleFor(x => x.Estado, x => x.Address.State())
                .RuleFor(x => x.DataNascimento, x => x.Date.Between(new DateTime(1990, 1, 1), new DateTime(2010, 12, 31)))
                .Generate(5)
                .Select(command => _mediator.Send(command));


            foreach (var taskAluno in taskAlunos)
            {
                await taskAluno;
            }


            var span = TimeSpan.FromSeconds(AddAlunoTimer.Minutes * 60);
            _timer = new Timer(ExecuteTask, null, span, span);
            _logger.LogInformation("Alunos inseridos com sucesso!");
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
        }
    }
}
