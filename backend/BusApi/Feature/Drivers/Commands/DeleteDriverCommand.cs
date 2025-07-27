using MediatR;

namespace BusApi.Feature.Drivers.Commands
{
    public record DeleteDriverCommand(Guid Id) : IRequest<Unit>;
}
