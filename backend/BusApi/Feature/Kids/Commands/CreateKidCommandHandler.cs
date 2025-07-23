using BusApi.Data;
using BusApi.Domain;
using MediatR;

namespace BusApi.Feature.Kids.Commands
{
    public class CreateKidCommandHandler : IRequestHandler<CreateKidCommand, Guid>
    {
        private readonly BusContext _context;

        public CreateKidCommandHandler(BusContext busContext) => _context = busContext;

        public async Task<Guid> Handle(CreateKidCommand request, CancellationToken cancellationToken)
        {
            var kid = new Kid { Name = request.Name, DocumentNumber = request.DocumentNumber, BusId = request.BusId };

            _context.Kids.Add(kid);
            await _context.SaveChangesAsync(cancellationToken);

            return kid.Id;
        }
    }
}