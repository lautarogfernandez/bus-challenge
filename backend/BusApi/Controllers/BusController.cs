using BusApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusController : ControllerBase
    {
        private List<BusResponse> _buses = new List<BusResponse>();

        private static readonly BusResponse[] InitialBuses =
        [
            new BusResponse { Id= 1, RegistrationPlate= "AA123ZZ", ChildrenIds= [1111,1112,1113], DriverId=100 },
            new BusResponse { Id= 2, RegistrationPlate= "BB355II", ChildrenIds= [111,1114], DriverId=100 },
            new BusResponse { Id= 3, RegistrationPlate= "AA874MN", ChildrenIds= [], DriverId=102 },
            new BusResponse { Id= 4, RegistrationPlate= "SD109PI", ChildrenIds= [1115, 1116, 1117, 1118, 1119], DriverId=101 }
        ];

        private readonly ILogger<BusController> _logger;

        public BusController(ILogger<BusController> logger)
        {
            _logger = logger;
            _buses = InitialBuses.ToList();
        }

        [HttpGet]
        public IEnumerable<BusListResponse> Get()
        {
            return _buses.Select(b => new BusListResponse
            {
                Id = b.Id,
                RegistrationPlate = b.RegistrationPlate,
                Driver = b.DriverId.ToString(),
                Children = b.ChildrenIds.Count()
            });
        }

        [HttpGet("{id}")]
        public ActionResult<BusListResponse> GetById(int id)
        {
            var bus = _buses.FirstOrDefault(b => b.Id == id);
            if (bus == null)
            {
                return NotFound($"Bus with Id {id} was not found");
            }
            return Ok(bus);
        }
    }
}