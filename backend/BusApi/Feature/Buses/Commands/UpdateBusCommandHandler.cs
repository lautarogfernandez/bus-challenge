using BusApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Feature.Buses.Commands
{
    public class UpdateBusCommandHandler : IRequestHandler<UpdateBusCommand, Unit>
    {
        private readonly ApplicationContext _context;

        public UpdateBusCommandHandler(ApplicationContext context) => _context = context;

        public async Task<Unit> Handle(UpdateBusCommand request, CancellationToken cancellationToken)
        {
            var bus = await _context.Buses
                .Include(b => b.Kids)
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (bus == null)
                throw new Exception($"Bus with Id {request.Id} not found.");

            bus.RegistrationPlate = request.RegistrationPlate;
            bus.DriverId = request.DriverId;

            var kids = await _context.Kids
                .Where(k => request.KidIds.Contains(k.Id))
                .ToListAsync(cancellationToken);

            bus.Kids.Clear();
            foreach (var kid in kids)
            {
                bus.Kids.Add(kid);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}