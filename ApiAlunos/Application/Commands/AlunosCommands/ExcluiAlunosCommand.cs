using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ApiAlunos.Infrastructure.Models;

namespace ApiAlunos.Application.Commands.AlunosCommands
{
    public class ExcluiAlunosCommand : IRequest<object>, ICommand
    {
        public int Id { get; set; }

        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        private class Validator : AbstractValidator<ExcluiAlunosCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Id).NotEmpty();
            }
        }
    }
}
