using BusApi.Repositories;
using MediatR;

namespace BusApi.Feature.Kids.Commands
{
    public class UpdateKidCommandHandler : IRequestHandler<UpdateKidCommand, Unit>
    {
        private readonly IKidRepository _kidRepository;

        public UpdateKidCommandHandler(IKidRepository kidRepository) => _kidRepository = kidRepository;

        public async Task<Unit> Handle(UpdateKidCommand request, CancellationToken cancellationToken)
        {
            var kid = await _kidRepository
                .GetByIdWithBusAsync(request.Id, cancellationToken);

            if (kid == null)
                throw new Exception($"Kid with Id {request.Id} not found.");

            kid.DocumentNumber = request.DocumentNumber;
            kid.Name = request.Name;

            await _kidRepository.UpdateAsync(kid, cancellationToken);

            return Unit.Value;
        }
    }
}