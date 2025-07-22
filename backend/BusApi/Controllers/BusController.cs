using BusApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusController : ControllerBase
    {
        private List<BusResponse> _buses = new List<BusResponse>();

        private static readonly BusResponse[] InitialBuses = new[]
        {
            new BusResponse { Id= 1, RegistrationPlate= "AA123ZZ", Children= 2, Driver="Héctor" },
            new BusResponse { Id= 2, RegistrationPlate= "BB355II", Children= 1, Driver="José" },
            new BusResponse { Id= 3, RegistrationPlate= "AA874MN", Children= 0, Driver="Juan" },
            new BusResponse { Id= 4, RegistrationPlate= "SD109PI", Children= 7, Driver="Quique" }
        };

        private readonly ILogger<BusController> _logger;

        public BusController(ILogger<BusController> logger)
        {
            _logger = logger;
            _buses = InitialBuses.ToList();
        }

        [HttpGet]
        public IEnumerable<BusResponse> Get()
        {
            return _buses;
        }
    }
}