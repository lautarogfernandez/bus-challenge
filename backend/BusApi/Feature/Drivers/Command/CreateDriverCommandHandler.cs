using BusApi.Data;
using BusApi.Domain;
using MediatR;

namespace BusApi.Feature.Drivers.Command
{
    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, Guid>
    {
        private readonly BusContext _context;

        public CreateDriverCommandHandler(BusContext busContext)
        {
            _context = busContext;
        }

        public async Task<Guid> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {
            var busesIds = request.BusesIds ?? [];
            var buses = _context.Buses.Where(x => busesIds.Contains(x.Id)).ToList();
            var driver = new Driver { DocumentNumber = request.DocumentNumber, Name = request.Name, Buses = buses };

            _context.Add(driver);
            await _context.SaveChangesAsync(cancellationToken);

            return driver.Id;
        }
    }
}