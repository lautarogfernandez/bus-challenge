using BusApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Feature.Kids.Commands
{
    public class UpdateKidCommandHandler : IRequestHandler<UpdateKidCommand, Unit>
    {
        private readonly ApplicationContext _context;

        public UpdateKidCommandHandler(ApplicationContext context) => _context = context;

        public async Task<Unit> Handle(UpdateKidCommand request, CancellationToken cancellationToken)
        {
            var kid = await _context.Kids
                .Include(b => b.Bus)
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (kid == null)
                throw new Exception($"Kid with Id {request.Id} not found.");

            kid.DocumentNumber = request.DocumentNumber;
            kid.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}