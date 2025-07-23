using BusApi.Data;
using BusApi.Domain;
using MediatR;

namespace BusApi.Feature.Drivers.Queries
{
    public class GetDriverByIdQueryHandler : IRequestHandler<GetDriverByIdQuery, Driver?>
    {
        private readonly BusContext _context;

        public GetDriverByIdQueryHandler(BusContext busContext) => _context = busContext;

        public async Task<Driver?> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
        {
            var driver = await _context.Drivers.FindAsync(request.Id);

            return driver;
        }
    }
}