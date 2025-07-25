using MediatR;

namespace Application.Feature.Drivers.Commands
{
    public record CreateDriverCommand(string DocumentNumber, string Name, Guid? BusId): IRequest<Guid>
    {
    }
}
