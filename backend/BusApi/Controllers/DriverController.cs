using BusApi.Feature.Buses.Commands;
using BusApi.Feature.Drivers.Commands;
using BusApi.Feature.Drivers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BusApi.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    public class DriverController : ControllerBase
    {
        private readonly ISender _sender;

        public DriverController(ISender sender) => _sender = sender;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var drivers = await _sender.Send(new GetAllDriverQuery());
            return Ok(drivers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var driver = await _sender.Send(new GetDriverByIdQuery(id));
            if (driver == null)
            {
                return NotFound($"Driver with Id {id} was not found");
            }
            return Ok(driver);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDriverCommand command)
        {
            var driverId = await _sender.Send(command);
            return Created(string.Empty, driverId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDriverCommand command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _sender.Send(new DeleteDriverCommand(id));
            return Ok();
        }
    }
}