using ApiAlunos.Application.Commands.AlunosCommands;
using ApiAlunos.Application.Queries.AlunosQuery;
using ApiAlunos.Application.Repositories.AlunosRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiAlunos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunosController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AlunosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromServices] IAlunosQuery query, [FromQuery] string nome = null)
        {
            return Ok(await query.ListAllAsync(nome));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromServices] IAlunosQuery query, int id)
        {
            return Ok(await query.ListByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CadastraAlunosCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(AlteraAlunosCommand command, int id)
        {
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cmd = new ExcluiAlunosCommand { Id = id };
            return Ok(await _mediator.Send(cmd));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var cmd = new ExcluirTodosAlunosCommand();
            return Ok(await _mediator.Send(cmd));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(AtualizaAlunosCommand command, int id)
        {
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }
    }
}