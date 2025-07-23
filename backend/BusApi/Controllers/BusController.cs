using BusApi.Data;
using BusApi.Domain;
using BusApi.Feature.Buses.Commands;
using BusApi.Feature.Buses.Queries;
using BusApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly BusContext _context;

        public BusController(BusContext busContext, ISender sender)
        {
            _context = busContext;
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var buses = await _sender.Send(new GetAllBusesQuery());
            return Ok(buses);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var bus = await _context.Buses.FindAsync(id);
            if (bus == null)
            {
                return NotFound($"Bus with Id {id} was not found");
            }
            return Ok(bus);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BusDto busDto)
        {
            var driver = await _context.Drivers.FindAsync(busDto.DriverId);
            var kids = await _context.Kids.Where(k => busDto.KidIds.Contains(k.Id)).ToListAsync();

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

            _context.Buses.Add(bus);
            await _context.SaveChangesAsync();
            return Created(string.Empty, bus.Id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _sender.Send(new DeleteBusCommand(id));
            return Ok();
        }
    }
}