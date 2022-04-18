using ApiAlunos.Application.Commands.HostedServicesCommands;
using ApiAlunos.Application.HostedServices.AddAluno;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiAlunos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                AddAlunoTimer.Minutes
            });
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AtualizaTimerCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
