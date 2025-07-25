using Application.Data;
using BusApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Feature.Drivers.Queries
{
    public class GetDriverByIdQueryHandler : IRequestHandler<GetDriverByIdQuery, DriverListResponse?>
    {
        private readonly ApplicationContext _context;

        public GetDriverByIdQueryHandler(ApplicationContext context) => _context = context;

        public async Task<DriverListResponse?> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
        {
            var driver = await _context.Drivers
                .Include(b => b.Bus)
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (driver == null)
                return null;

            var response = new DriverListResponse
            {
                Id = driver.Id,
                Name = driver.Name,
                DocumentNumber = driver.DocumentNumber,
                BusRegistrationPlate = driver.Bus?.RegistrationPlate
            };

            return response;
        }
    }
}