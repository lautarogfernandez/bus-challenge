using BusApi.Data;
using MediatR;

namespace BusApi.Feature.Kids.Commands
{
    public class DeleteKidCommandHandler : IRequestHandler<DeleteKidCommand, Unit>
    {
        private readonly ApplicationContext _context;

        public DeleteKidCommandHandler(ApplicationContext context) => _context = context;

        public async Task<Unit> Handle(DeleteKidCommand request, CancellationToken cancellationToken)
        {
            var kid = await _context.Kids.FindAsync(request.Id);
            if (kid == null)
            {
                throw new Exception($"Kid with Id {request.Id} not found.");
            }

            _context.Kids.Remove(kid);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}