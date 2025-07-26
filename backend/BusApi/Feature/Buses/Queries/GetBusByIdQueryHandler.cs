using BusApi.Models;
using BusApi.Repositories;
using MediatR;

namespace BusApi.Feature.Buses.Queries
{
    public class GetBusByIdQueryHandler : IRequestHandler<GetBusByIdQuery, BusResponse?>
    {
        private readonly IBusRepository _busRepository;

        public GetBusByIdQueryHandler(IBusRepository busRepository) => _busRepository = busRepository;

        public async Task<BusResponse?> Handle(GetBusByIdQuery request, CancellationToken cancellationToken)
        {
            var bus = await _busRepository.GetByIdWithKidsAsync(request.Id, cancellationToken);

            if (bus == null)
                return null;

            var response = new BusResponse
            {
                Id = bus.Id,
                RegistrationPlate = bus.RegistrationPlate,
                DriverId = bus.DriverId,
                KidIds = bus.Kids?.Select(k => k.Id).ToList() ?? new List<Guid>()
            };

            return response;
        }
    }
}