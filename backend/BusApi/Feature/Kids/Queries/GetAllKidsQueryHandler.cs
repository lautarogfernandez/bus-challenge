using BusApi.Data;
using BusApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Feature.Kids.Queries
{
    public class GetAllKidsQueryHandler : IRequestHandler<GetAllKidsQuery, IEnumerable<KidListResponse>>
    {
        private readonly BusContext _context;

        public GetAllKidsQueryHandler(BusContext busContext) => _context = busContext;

        public async Task<IEnumerable<KidListResponse>> Handle(GetAllKidsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Kids
                .Select(b => new KidListResponse
                {
                    Id = b.Id,
                    DocumentNumber = b.DocumentNumber,
                    Name = b.Name,
                    BusRegistrationPlate = b.Bus.RegistrationPlate
                }).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}