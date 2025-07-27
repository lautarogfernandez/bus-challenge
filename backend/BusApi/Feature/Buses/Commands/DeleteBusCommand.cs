using MediatR;

namespace BusApi.Feature.Buses.Commands
{
    public record DeleteBusCommand(Guid Id) : IRequest<Unit>;
}
