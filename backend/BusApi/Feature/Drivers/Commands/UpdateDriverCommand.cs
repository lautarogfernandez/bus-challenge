using MediatR;

namespace BusApi.Feature.Drivers.Commands
{
    public record UpdateDriverCommand(Guid Id, string DocumentNumber, string Name) : IRequest<Unit>
    {

    }
}
