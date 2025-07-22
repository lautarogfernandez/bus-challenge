using BusApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChildrenController : ControllerBase
    {
        private List<ChildResponse> _children = new List<ChildResponse>();

        private static readonly ChildResponse[] InitialChildren =
        [
            new ChildResponse { Id= 1111, DocumentNumber= "50098653", Name = "Juancito" },
            new ChildResponse { Id= 1112, DocumentNumber= "47123654", Name= "Agustin"},
            new ChildResponse { Id= 1113, DocumentNumber= "49098198", Name = "Felipe" },
            new ChildResponse { Id= 1114, DocumentNumber= "48987351", Name = "Juana" },
            new ChildResponse { Id= 1115, DocumentNumber= "51876202", Name = "María" },
            new ChildResponse { Id= 1116, DocumentNumber= "51654212", Name = "Natalia" },
            new ChildResponse { Id= 1117, DocumentNumber= "51216543", Name = "Federico" },
            new ChildResponse { Id= 1118, DocumentNumber= "52116400", Name = "Santiago" },
            new ChildResponse { Id= 1119, DocumentNumber= "53542498", Name = "Bianca" },
        ];

        private readonly ILogger<ChildrenController> _logger;

        public ChildrenController(ILogger<ChildrenController> logger)
        {
            _logger = logger;
            _children = InitialChildren.ToList();
        }

        [HttpGet]
        public IEnumerable<ChildResponse> Get()
        {
            return _children;
        }

        [HttpGet("{id}")]
        public ActionResult<ChildResponse> GetById(int id)
        {
            var driver = _children.FirstOrDefault(b => b.Id == id);
            if (driver == null)
            {
                return NotFound($"Child with Id {id} was not found");
            }
            return Ok(driver);
        }
    }
}