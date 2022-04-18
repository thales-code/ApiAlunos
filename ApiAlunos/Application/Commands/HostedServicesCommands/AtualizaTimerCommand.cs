using ApiAlunos.Infrastructure.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;


namespace ApiAlunos.Application.Commands.HostedServicesCommands
{
    public class AtualizaTimerCommand : IRequest<object>, ICommand
    {
        public int Minutes { get; set; }

        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        private class Validator : AbstractValidator<AtualizaTimerCommand> {
            public Validator() {
                RuleFor(x => x.Minutes).GreaterThan(0);
            }
        }
    }
}
