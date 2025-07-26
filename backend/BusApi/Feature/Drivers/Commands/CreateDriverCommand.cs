using MediatR;

namespace BusApi.Feature.Drivers.Commands
{
    public record CreateDriverCommand(string DocumentNumber, string Name): IRequest<Guid>
    {
    }
}
