using BusApi.Repositories;
using MediatR;

namespace BusApi.Feature.Drivers.Commands
{
    public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, Unit>
    {
        private readonly IDriverRepository _driverRepository;

        public UpdateDriverCommandHandler(IDriverRepository driverRepository) => _driverRepository = driverRepository;

        public async Task<Unit> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.GetByIdWithBusAsync(request.Id, cancellationToken);

            if (driver == null)
                throw new Exception($"Driver with Id {request.Id} not found.");

            driver.DocumentNumber = request.DocumentNumber;
            driver.Name = request.Name;

            await _driverRepository.UpdateAsync(driver, cancellationToken);

            return Unit.Value;
        }
    }
}