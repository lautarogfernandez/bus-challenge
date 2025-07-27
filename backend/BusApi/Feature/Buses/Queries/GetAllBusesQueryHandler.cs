using BusApi.Models;
using BusApi.Repositories;
using MediatR;

namespace BusApi.Feature.Buses.Queries
{
    public class GetAllBusesQueryHandler : IRequestHandler<GetAllBusesQuery, IEnumerable<BusListResponse>>
    {
        private readonly IBusRepository _busRepository;

        public GetAllBusesQueryHandler(IBusRepository busRepository) => _busRepository = busRepository;

        public async Task<IEnumerable<BusListResponse>> Handle(GetAllBusesQuery request, CancellationToken cancellationToken)
        {
            return await _busRepository.GetAllListAsync(cancellationToken);
        }
    }
}