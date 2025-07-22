using BusApi.Data;
using BusApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KidController : ControllerBase
    {
        private readonly ILogger<KidController> _logger;
        private readonly BusContext _busContext;

        public KidController(BusContext busContext, ILogger<KidController> logger)
        {
            _logger = logger;
            _busContext = busContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var kids = await _busContext.Kids.ToListAsync();
            return Ok(kids);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var kid = await _busContext.Kids.FindAsync(id);
            if (kid == null)
            {
                return NotFound($"Kid with Id {id} was not found");
            }
            return Ok(kid);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Kid kid)
        {
            _busContext.Kids.Add(kid);
            await _busContext.SaveChangesAsync();
            return Created(string.Empty, kid.Id);
        }
    }
}