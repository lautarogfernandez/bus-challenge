using BusApi.Domain;
using BusApi.Repositories;
using MediatR;

namespace BusApi.Feature.Buses.Commands
{
    public class UpdateBusCommandHandler : IRequestHandler<UpdateBusCommand, Unit>
    {
        private readonly IBusRepository _busRepository;
        private readonly IRepository<Kid> _kidRepository;

        public UpdateBusCommandHandler(IBusRepository busRepository, IRepository<Kid> kidRepository)
        {
            _busRepository = busRepository;
            _kidRepository = kidRepository;
        }

        public async Task<Unit> Handle(UpdateBusCommand request, CancellationToken cancellationToken)
        {
            var bus = await _busRepository.GetByIdWithKidsAsync(request.Id, cancellationToken);
            if (bus == null)
                throw new Exception($"Bus with Id {request.Id} not found.");

            bus.RegistrationPlate = request.RegistrationPlate;
            bus.DriverId = request.DriverId;

            var kids = await _kidRepository
                .FindAsync(k => request.KidIds.Contains(k.Id), cancellationToken);

            bus.Kids ??= new List<Kid>();
            bus.Kids.Clear();
            foreach (var kid in kids)
            {
                bus.Kids.Add(kid);
            }

            await _busRepository.UpdateAsync(bus, cancellationToken);

            return Unit.Value;
        }
    }
}