using Application.Data;
using Domain.Entities;
using MediatR;

namespace Application.Feature.Kids.Commands
{
    public class CreateKidCommandHandler : IRequestHandler<CreateKidCommand, Guid>
    {
        private readonly ApplicationContext _context;

        public CreateKidCommandHandler(ApplicationContext context) => _context = context;

        public async Task<Guid> Handle(CreateKidCommand request, CancellationToken cancellationToken)
        {
            var kid = new Kid { Name = request.Name, DocumentNumber = request.DocumentNumber, BusId = request.BusId };

            _context.Kids.Add(kid);
            await _context.SaveChangesAsync(cancellationToken);

            return kid.Id;
        }
    }
}