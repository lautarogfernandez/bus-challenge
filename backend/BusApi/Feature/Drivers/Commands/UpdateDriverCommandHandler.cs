using BusApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Feature.Drivers.Commands
{
    public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, Unit>
    {
        private readonly ApplicationContext _context;

        public UpdateDriverCommandHandler(ApplicationContext context) => _context = context;

        public async Task<Unit> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = await _context.Drivers
                .Include(b => b.Bus)
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (driver == null)
                throw new Exception($"Driver with Id {request.Id} not found.");

            driver.DocumentNumber = request.DocumentNumber;
            driver.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}