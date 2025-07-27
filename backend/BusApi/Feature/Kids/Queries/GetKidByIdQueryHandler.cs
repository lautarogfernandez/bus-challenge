using BusApi.Models;
using BusApi.Repositories;
using MediatR;

namespace BusApi.Feature.Kids.Queries
{
    public class GetKidByIdQueryHandler : IRequestHandler<GetKidByIdQuery, KidListResponse?>
    {
        private readonly IKidRepository _kidRepository;

        public GetKidByIdQueryHandler(IKidRepository kidRepository) => _kidRepository = kidRepository;

        public async Task<KidListResponse?> Handle(GetKidByIdQuery request, CancellationToken cancellationToken)
        {
            var kid = await _kidRepository.GetByIdWithBusAsync(request.Id, cancellationToken);

            if (kid == null)
                return null;

            var response = new KidListResponse
            {
                Id = kid.Id,
                Name = kid.Name,
                DocumentNumber = kid.DocumentNumber,
                BusRegistrationPlate = kid.Bus?.RegistrationPlate
            };

            return response;
        }
    }
}