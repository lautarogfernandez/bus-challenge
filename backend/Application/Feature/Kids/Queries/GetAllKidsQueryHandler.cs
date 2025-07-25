using Application.Data;
using BusApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Feature.Kids.Queries
{
    public class GetAllKidsQueryHandler : IRequestHandler<GetAllKidsQuery, IEnumerable<KidListResponse>>
    {
        private readonly ApplicationContext _context;

        public GetAllKidsQueryHandler(ApplicationContext context) => _context = context;

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