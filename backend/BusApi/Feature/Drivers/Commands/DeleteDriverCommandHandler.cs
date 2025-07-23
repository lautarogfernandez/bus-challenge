using BusApi.Data;
using MediatR;

namespace BusApi.Feature.Drivers.Commands
{
    public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, Unit>
    {
        private readonly BusContext _context;

        public DeleteDriverCommandHandler(BusContext busContext) => _context = busContext;

        public async Task<Unit> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = await _context.Drivers.FindAsync(request.Id);
            if (driver == null)
            {
                throw new Exception($"Driver with Id {request.Id} not found.");
            }

            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}