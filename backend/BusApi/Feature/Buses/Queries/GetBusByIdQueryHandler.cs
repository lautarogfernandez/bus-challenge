using BusApi.Data;
using BusApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Feature.Buses.Queries
{
    public class GetBusByIdQueryHandler : IRequestHandler<GetBusByIdQuery, BusResponse?>
    {
        private readonly ApplicationContext _context;

        public GetBusByIdQueryHandler(ApplicationContext context) => _context = context;

        public async Task<BusResponse?> Handle(GetBusByIdQuery request, CancellationToken cancellationToken)
        {
            var bus = await _context.Buses
                .Include(b => b.Kids)
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (bus == null)
                return null;

            var response = new BusResponse
            {
                Id = bus.Id,
                RegistrationPlate = bus.RegistrationPlate,
                DriverId = bus.DriverId,
                KidIds = bus.Kids?.Select(k => k.Id).ToList() ?? new List<Guid>()
            };

            return response;
        }
    }
}