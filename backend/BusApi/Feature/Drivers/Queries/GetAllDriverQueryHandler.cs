using BusApi.Data;
using BusApi.Domain;
using MediatR;

namespace BusApi.Feature.Drivers.Queries
{
    public class GetAllDriverQueryHandler : IRequestHandler<GetAllDriverQuery, IEnumerable<Driver>>
    {
        private readonly BusContext _context;

        public GetAllDriverQueryHandler(BusContext busContext)
        {
            _context = busContext;
        }

        public async Task<IEnumerable<Driver>> Handle(GetAllDriverQuery request, CancellationToken cancellationToken)
        {
            return _context.Drivers;
        }
    }
}   