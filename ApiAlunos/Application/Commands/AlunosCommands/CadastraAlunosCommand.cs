using ApiAlunos.Domain.Entities;
using ApiAlunos.Infrastructure.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ApiAlunos.Application.Commands.AlunosCommands
{
    public class CadastraAlunosCommand : IRequest<Alunos>, ICommand
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        private class Validator : AbstractValidator<CadastraAlunosCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Nome).NotEmpty();
                RuleFor(c => c.DataNascimento).NotEmpty();
                RuleFor(c => c.Estado).NotEmpty();
                RuleFor(c => c.Cidade).NotEmpty();
            }
        }
    }
}