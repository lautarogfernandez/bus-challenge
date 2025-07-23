using MediatR;

namespace BusApi.Feature.Drivers.Command
{
    public record CreateDriverCommand(string DocumentNumber, string Name, Guid? BusId): IRequest<Guid>
    {
    }
}
