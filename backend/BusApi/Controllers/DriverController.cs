using BusApi.Data;
using BusApi.Domain;
using BusApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly ILogger<DriverController> _logger;
        private readonly BusContext _busContext;

        public DriverController(BusContext busContext, ILogger<DriverController> logger)
        {
            _logger = logger;
            _busContext = busContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var driver = await _busContext.Drivers.ToListAsync();
            return Ok(driver);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var driver = await _busContext.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound($"Driver with Id {id} was not found");
            }
            return Ok(driver);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Driver driver)
        {
            _busContext.Drivers.Add(driver);
            await _busContext.SaveChangesAsync();
            return Created(string.Empty, driver.Id);
        }
    }
}