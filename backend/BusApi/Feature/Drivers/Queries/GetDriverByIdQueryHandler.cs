using BusApi.Models;
using BusApi.Repositories;
using MediatR;

namespace BusApi.Feature.Drivers.Queries
{
    public class GetDriverByIdQueryHandler : IRequestHandler<GetDriverByIdQuery, DriverListResponse?>
    {
        private readonly IDriverRepository _driverRepository;

        public GetDriverByIdQueryHandler(IDriverRepository driverRepository) => _driverRepository = driverRepository;

        public async Task<DriverListResponse?> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.GetByIdWithBusAsync(request.Id, cancellationToken);

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