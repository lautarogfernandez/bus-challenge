using BusApi.Models;
using BusApi.Repositories;
using MediatR;

namespace BusApi.Feature.Drivers.Queries
{
    public class GetAllDriverQueryHandler : IRequestHandler<GetAllDriverQuery, IEnumerable<DriverListResponse>>
    {
        private readonly IDriverRepository _driverRepository;

        public GetAllDriverQueryHandler(IDriverRepository driverRepository) => _driverRepository = driverRepository;

        public async Task<IEnumerable<DriverListResponse>> Handle(GetAllDriverQuery request, CancellationToken cancellationToken)
        {
            return await _driverRepository.GetAllListAsync(cancellationToken);
        }
    }
}