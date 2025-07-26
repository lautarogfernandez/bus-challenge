using BusApi.Repositories;
using MediatR;

namespace BusApi.Feature.Kids.Commands
{
    public class DeleteKidCommandHandler : IRequestHandler<DeleteKidCommand, Unit>
    {
        private readonly IKidRepository _kidRepository;

        public DeleteKidCommandHandler(IKidRepository kidRepository) => _kidRepository = kidRepository;

        public async Task<Unit> Handle(DeleteKidCommand request, CancellationToken cancellationToken)
        {
            var kid = await _kidRepository.GetByIdAsync(request.Id, cancellationToken);
            if (kid == null)
            {
                throw new Exception($"Kid with Id {request.Id} not found.");
            }

            await _kidRepository.DeleteAsync(kid, cancellationToken);

            return Unit.Value;
        }
    }
}