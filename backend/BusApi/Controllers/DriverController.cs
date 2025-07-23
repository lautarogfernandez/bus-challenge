using BusApi.Data;
using BusApi.Feature.Drivers.Command;
using BusApi.Feature.Drivers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BusApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly ILogger<DriverController> _logger;
        private readonly BusContext _busContext;
        private readonly ISender _sender;

        public DriverController(BusContext busContext, ILogger<DriverController> logger, ISender sender)
        {
            _logger = logger;
            _busContext = busContext;
            _sender = sender;
        }

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
    }
}