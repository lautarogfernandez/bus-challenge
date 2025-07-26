using BusApi.Domain;
using BusApi.Repositories;
using MediatR;

namespace BusApi.Feature.Buses.Commands
{
    public class CreateBusCommandHandler : IRequestHandler<CreateBusCommand, Guid>
    {
        private readonly IRepository<Bus> _busRepository;
        private readonly IRepository<Kid> _kidRepository;

        public CreateBusCommandHandler(IRepository<Bus> busRepository, IRepository<Kid> kidRepository)
        {
            _busRepository = busRepository;
            _kidRepository = kidRepository;
        }

        public async Task<Guid> Handle(CreateBusCommand request, CancellationToken cancellationToken)
        {
            var kids = (await _kidRepository.GetAllAsync(cancellationToken))
                .Where(k => request.KidIds.Contains(k.Id))
                .ToList();

            var bus = new Bus
            {
                RegistrationPlate = request.RegistrationPlate,
                DriverId = request.DriverId,
                Kids = kids
            };

            await _busRepository.CreateAsync(bus, cancellationToken);

            return bus.Id;
        }
    }
}