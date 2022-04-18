using ApiAlunos.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using ApiAlunos.Infrastructure.Models;


namespace ApiAlunos.Application.Commands.AlunosCommands
{
    public class AtualizaAlunosCommand : IRequest<Alunos>, ICommand
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public DateTime DataMatricula { get; set; }

        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        private class Validator : AbstractValidator<AtualizaAlunosCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Id).NotEmpty();
            }
        }
    }
}
