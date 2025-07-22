using BusApi.Data;
using BusApi.Domain;
using BusApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusController : ControllerBase
    {
        private readonly ILogger<BusController> _logger;
        private readonly BusContext _busContext;

        public BusController(BusContext busContext, ILogger<BusController> logger)
        {
            _logger = logger;
            _busContext = busContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var buses = await _busContext.Buses.ToListAsync();
            return Ok(buses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var bus = await _busContext.Buses.FindAsync(id);
            if (bus == null)
            {
                return NotFound($"Bus with Id {id} was not found");
            }
            return Ok(bus);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BusDto busDto)
        {
            var driver = await _busContext.Drivers.FindAsync(busDto.DriverId);
            var kids = await _busContext.Kids.Where(k => busDto.KidIds.Contains(k.Id)).ToListAsync();

            if (driver == null)
            {
                return NotFound($"Driver was not found");
            }

            var bus = new Bus
            {
                RegistrationPlate = busDto.RegistrationPlate,
                Driver = driver,
                Kids = kids
            };

            _busContext.Buses.Add(bus);
            await _busContext.SaveChangesAsync();
            return Created(string.Empty, bus.Id);
        }
    }
}