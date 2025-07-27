using BusApi.Repositories;
using MediatR;

namespace BusApi.Feature.Buses.Commands
{
    public class DeleteBusCommandHandler : IRequestHandler<DeleteBusCommand, Unit>
    {
        private readonly IBusRepository _busRepository;

        public DeleteBusCommandHandler(IBusRepository busRepository) => _busRepository = busRepository;

        public async Task<Unit> Handle(DeleteBusCommand request, CancellationToken cancellationToken)
        {
            var bus = await _busRepository.GetByIdAsync(request.Id, cancellationToken);
            if (bus == null)
            {
                throw new Exception($"Bus with Id {request.Id} not found.");
            }

            await _busRepository.DeleteAsync(bus, cancellationToken);

            return Unit.Value;
        }
    }
}