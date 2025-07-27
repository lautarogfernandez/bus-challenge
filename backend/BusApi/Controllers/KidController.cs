using BusApi.Feature.Kids.Commands;
using BusApi.Feature.Kids.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BusApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KidController : ControllerBase
    {
        private readonly ISender _sender;

        public KidController(ISender sender) => _sender = sender;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var kids = await _sender.Send(new GetAllKidsQuery());
            return Ok(kids);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var kid = await _sender.Send(new GetKidByIdQuery(id));
            if (kid == null)
            {
                return NotFound($"Kid with Id {id} was not found");
            }
            return Ok(kid);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateKidCommand command)
        {
            var kidId = await _sender.Send(command);
            return Created(string.Empty, kidId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateKidCommand command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _sender.Send(new DeleteKidCommand(id));
            return Ok();
        }
    }
}