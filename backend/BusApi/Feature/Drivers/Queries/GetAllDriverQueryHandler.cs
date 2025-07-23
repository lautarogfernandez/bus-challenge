using BusApi.Data;
using BusApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Feature.Drivers.Queries
{
    public class GetAllDriverQueryHandler : IRequestHandler<GetAllDriverQuery, IEnumerable<DriverListResponse>>
    {
        private readonly BusContext _context;

        public GetAllDriverQueryHandler(BusContext busContext) => _context = busContext;

        public async Task<IEnumerable<DriverListResponse>> Handle(GetAllDriverQuery request, CancellationToken cancellationToken)
        {
            return await _context.Drivers
                .Select(d => new DriverListResponse
                {
                    Id = d.Id,
                    Name = d.Name,
                    DocumentNumber = d.DocumentNumber,
                    BusRegistrationPlate = d.Bus != null ? d.Bus.RegistrationPlate : null
                }).ToListAsync();
        }
    }
}