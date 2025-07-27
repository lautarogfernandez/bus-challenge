using BusApi.Repositories;
using MediatR;

namespace BusApi.Feature.Drivers.Commands
{
    public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, Unit>
    {
        private readonly IDriverRepository _driverRepository;

        public DeleteDriverCommandHandler(IDriverRepository driverRepository) => _driverRepository = driverRepository;

        public async Task<Unit> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.GetByIdAsync(request.Id, cancellationToken);
            if (driver == null)
            {
                throw new Exception($"Driver with Id {request.Id} not found.");
            }

            await _driverRepository.DeleteAsync(driver, cancellationToken);

            return Unit.Value;
        }
    }
}