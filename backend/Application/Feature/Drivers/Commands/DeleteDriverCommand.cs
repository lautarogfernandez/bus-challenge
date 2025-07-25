using MediatR;

namespace Application.Feature.Drivers.Commands
{
    public record DeleteDriverCommand(Guid Id) : IRequest<Unit>;
}
