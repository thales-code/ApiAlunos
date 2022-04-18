using ApiAlunos.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using ApiAlunos.Infrastructure.Models;

namespace ApiAlunos.Application.Commands.AlunosCommands
{
    public class AlteraAlunosCommand : IRequest<Alunos>, ICommand
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

        private class Validator : AbstractValidator<AlteraAlunosCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Id).NotEmpty();
                RuleFor(c => c.Nome).NotEmpty();
                RuleFor(c => c.DataNascimento).NotEmpty();
                RuleFor(c => c.DataMatricula).NotEmpty();
                RuleFor(c => c.Estado).NotEmpty();
                RuleFor(c => c.DataMatricula).NotEmpty();
            }
        }
    }
}
