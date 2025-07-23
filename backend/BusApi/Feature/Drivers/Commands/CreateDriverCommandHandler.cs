using BusApi.Data;
using BusApi.Domain;
using MediatR;

namespace BusApi.Feature.Drivers.Commands
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
            var bus = await _context.Buses.FindAsync(request.BusId);
            var driver = new Driver { DocumentNumber = request.DocumentNumber, Name = request.Name, Bus = bus };

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync(cancellationToken);

            return driver.Id;
        }
    }
}