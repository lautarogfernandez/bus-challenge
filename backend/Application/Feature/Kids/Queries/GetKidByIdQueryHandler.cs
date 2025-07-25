using Application.Data;
using BusApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Feature.Kids.Queries
{
    public class GetKidByIdQueryHandler : IRequestHandler<GetKidByIdQuery, KidListResponse?>
    {
        private readonly ApplicationContext _context;

        public GetKidByIdQueryHandler(ApplicationContext context) => _context = context;

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