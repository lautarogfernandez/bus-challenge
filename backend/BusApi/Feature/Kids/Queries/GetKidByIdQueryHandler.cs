using BusApi.Data;
using BusApi.Domain;
using MediatR;

namespace BusApi.Feature.Kids.Queries
{
    public class GetKidByIdQueryHandler : IRequestHandler<GetKidByIdQuery, Kid?>
    {
        private readonly BusContext _context;

        public GetKidByIdQueryHandler(BusContext busContext) => _context = busContext;

        public async Task<Kid?> Handle(GetKidByIdQuery request, CancellationToken cancellationToken)
        {
            var kid = await _context.Kids.FindAsync(request.Id);

            return kid;
        }
    }
}