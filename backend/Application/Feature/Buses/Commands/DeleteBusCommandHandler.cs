using Application.Data;
using MediatR;

namespace Application.Feature.Buses.Commands
{
    public class DeleteBusCommandHandler : IRequestHandler<DeleteBusCommand, Unit>
    {
        private readonly ApplicationContext _context;

        public DeleteBusCommandHandler(ApplicationContext context) => _context = context;

        public async Task<Unit> Handle(DeleteBusCommand request, CancellationToken cancellationToken)
        {
            var bus = await _context.Buses.FindAsync(request.Id);
            if (bus == null)
            {
                throw new Exception($"Bus with Id {request.Id} not found.");
            }

            _context.Buses.Remove(bus);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}