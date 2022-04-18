using FluentValidation.Results;

namespace ApiAlunos.Infrastructure.Models
{
    public interface ICommand
    {
        public ValidationResult Validate();
    }
}
