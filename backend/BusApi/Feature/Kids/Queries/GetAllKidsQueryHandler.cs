using BusApi.Models;
using BusApi.Repositories;
using MediatR;

namespace BusApi.Feature.Kids.Queries
{
    public class GetAllKidsQueryHandler : IRequestHandler<GetAllKidsQuery, IEnumerable<KidListResponse>>
    {
        private readonly IKidRepository _kidRepository;

        public GetAllKidsQueryHandler(IKidRepository kidRepository) => _kidRepository = kidRepository;

        public async Task<IEnumerable<KidListResponse>> Handle(GetAllKidsQuery request, CancellationToken cancellationToken)
        {
            return await _kidRepository.GetAllListAsync(cancellationToken);
        }
    }
}