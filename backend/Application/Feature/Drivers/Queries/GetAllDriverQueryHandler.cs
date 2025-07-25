using Application.Data;
using BusApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Feature.Drivers.Queries
{
    public class GetAllDriverQueryHandler : IRequestHandler<GetAllDriverQuery, IEnumerable<DriverListResponse>>
    {
        private readonly ApplicationContext _context;

        public GetAllDriverQueryHandler(ApplicationContext context) => _context = context;

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