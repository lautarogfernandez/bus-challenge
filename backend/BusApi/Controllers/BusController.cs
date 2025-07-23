using BusApi.Feature.Buses.Commands;
using BusApi.Feature.Buses.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BusApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusController : ControllerBase
    {
        private readonly ISender _sender;

        public BusController(ISender sender) => _sender = sender;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var buses = await _sender.Send(new GetAllBusesQuery());
            return Ok(buses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var bus = await _sender.Send(new GetBusByIdQuery(id));
            if (bus == null)
            {
                return NotFound($"Bus with Id {id} was not found");
            }
            return Ok(bus);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBusCommand command)
        {
            var busId = await _sender.Send(command);
            return Created(string.Empty, busId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _sender.Send(new DeleteBusCommand(id));
            return Ok();
        }
    }
}