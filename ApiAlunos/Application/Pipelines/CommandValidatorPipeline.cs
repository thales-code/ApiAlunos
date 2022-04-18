using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiAlunos.Infrastructure.Models;


namespace ApiAlunos.Application.Pipelines
{
    public class CommandValidatorPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICommand
        where TResponse : class
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var command = (ICommand)request;
            var validationResult = command.Validate();

            var failures = validationResult.Errors?
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                var errors = string.Join("\r\n", failures);
                throw new Exception(errors);
            }

            return next();
        }
    }
}
