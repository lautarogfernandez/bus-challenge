using BusApi.Data;
using BusApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Feature.Buses.Queries
{
    public class GetAllBusesQueryHandler : IRequestHandler<GetAllBusesQuery, IEnumerable<BusListResponse>>
    {
        private readonly ApplicationContext _context;

        public GetAllBusesQueryHandler(ApplicationContext context) => _context = context;

        public async Task<IEnumerable<BusListResponse>> Handle(GetAllBusesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Buses
                .Select(b => new BusListResponse
                {
                    Id = b.Id,
                    RegistrationPlate = b.RegistrationPlate,
                    DriverDocumentNumber = b.Driver.DocumentNumber,
                    Kids = _context.Kids.Count(k => k.BusId == b.Id)
                }).ToListAsync();
        }
    }
}