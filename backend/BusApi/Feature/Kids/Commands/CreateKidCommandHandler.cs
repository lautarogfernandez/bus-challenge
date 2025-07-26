using BusApi.Domain;
using BusApi.Repositories;
using MediatR;

namespace BusApi.Feature.Kids.Commands
{
    public class CreateKidCommandHandler : IRequestHandler<CreateKidCommand, Guid>
    {
        private readonly IKidRepository _kidRepository;

        public CreateKidCommandHandler(IKidRepository kidRepository) => _kidRepository = kidRepository;

        public async Task<Guid> Handle(CreateKidCommand request, CancellationToken cancellationToken)
        {
            var kid = new Kid
            {
                Name = request.Name,
                DocumentNumber = request.DocumentNumber
            };

            await _kidRepository.CreateAsync(kid, cancellationToken);

            return kid.Id;
        }
    }
}