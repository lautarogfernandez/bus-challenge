using BusApi.Data;
using BusApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Feature.Kids.Queries
{
    public class GetKidByIdQueryHandler : IRequestHandler<GetKidByIdQuery, KidListResponse?>
    {
        private readonly BusContext _context;

        public GetKidByIdQueryHandler(BusContext busContext) => _context = busContext;

        public async Task<KidListResponse?> Handle(GetKidByIdQuery request, CancellationToken cancellationToken)
        {
            var kid = await _context.Kids
                .Include(b => b.Bus)
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (kid == null)
                return null;

            var response = new KidListResponse
            {
                Id = kid.Id,
                Name = kid.Name,
                DocumentNumber = kid.DocumentNumber,
                BusRegistrationPlate = kid.Bus?.RegistrationPlate
            };

            return response;
        }
    }
}