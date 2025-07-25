using MediatR;

namespace Application.Feature.Buses.Commands
{
    public record DeleteBusCommand(Guid Id) : IRequest<Unit>;
}
