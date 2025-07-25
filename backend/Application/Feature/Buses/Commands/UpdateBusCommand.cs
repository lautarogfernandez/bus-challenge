using MediatR;

namespace Application.Feature.Buses.Commands
{
    public record UpdateBusCommand(Guid Id, string RegistrationPlate, Guid DriverId, IEnumerable<Guid>? KidIds) : IRequest<Unit>
    {

    }
}
