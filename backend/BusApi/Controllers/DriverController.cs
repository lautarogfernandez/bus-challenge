using BusApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
        private List<DriverResponse> _drivers = new List<DriverResponse>();

        private static readonly DriverResponse[] InitialDrivers =
        [
            new DriverResponse { Id= 100, DocumentNumber= "14945678", Name= "José"},
            new DriverResponse { Id= 101, DocumentNumber= "36073871", Name = "Héctor" },
            new DriverResponse { Id= 102, DocumentNumber= "37543098", Name = "Rául" },
            new DriverResponse { Id= 103, DocumentNumber= "17098343", Name = "Olga" },
            new DriverResponse { Id= 104, DocumentNumber= "23450982", Name = "Carmen" },
        ];

        private readonly ILogger<DriverController> _logger;

        public DriverController(ILogger<DriverController> logger)
        {
            _logger = logger;
            _drivers = InitialDrivers.ToList();
        }

        [HttpGet]
        public IEnumerable<DriverResponse> Get()
        {
            return _drivers;
        }

        [HttpGet("{id}")]
        public ActionResult<DriverResponse> GetById(int id)
        {
            var driver = _drivers.FirstOrDefault(b => b.Id == id);
            if (driver == null)
            {
                return NotFound($"Driver with Id {id} was not found");
            }
            return Ok(driver);
        }
    }
}