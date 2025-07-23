using BusApi.Data;
using BusApi.Domain;
using MediatR;

namespace BusApi.Feature.Buses.Queries
{
    public class GetBusByIdQueryHandler : IRequestHandler<GetBusByIdQuery, Bus?>
    {
        private readonly BusContext _context;

        public GetBusByIdQueryHandler(BusContext busContext) => _context = busContext;

        public async Task<Bus?> Handle(GetBusByIdQuery request, CancellationToken cancellationToken)
        {
            var bus = await _context.Buses.FindAsync(request.Id);

            return bus;
        }
    }
}