using BusApi.Data;
using BusApi.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Feature.Buses.Commands
{
    public class CreateBusCommandHandler : IRequestHandler<CreateBusCommand, Guid>
    {
        private readonly BusContext _context;

        public CreateBusCommandHandler(BusContext busContext)
        {
            _context = busContext;
        }

        public async Task<Guid> Handle(CreateBusCommand request, CancellationToken cancellationToken)
        {
            var kids = await _context.Kids
             .Where(k => request.KidIds.Contains(k.Id))
             .ToListAsync(cancellationToken);
            var bus = new Bus { RegistrationPlate = request.RegistrationPlate, DriverId = request.DriverId, Kids = kids };

            _context.Buses.Add(bus);
            await _context.SaveChangesAsync(cancellationToken);

            return bus.Id;
        }
    }
}