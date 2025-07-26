using BusApi.Domain;
using BusApi.Repositories;
using MediatR;

namespace BusApi.Feature.Drivers.Commands
{
    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, Guid>
    {
        private readonly IDriverRepository _driverRepository;

        public CreateDriverCommandHandler(IDriverRepository driverRepository) => _driverRepository = driverRepository;

        public async Task<Guid> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = new Driver
            {
                DocumentNumber = request.DocumentNumber,
                Name = request.Name
            };

            await _driverRepository.UpdateAsync(driver, cancellationToken);

            return driver.Id;
        }
    }
}