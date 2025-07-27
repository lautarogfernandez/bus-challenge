using MediatR;

namespace BusApi.Feature.Buses.Commands
{
    public record CreateBusCommand(string RegistrationPlate, Guid DriverId, IEnumerable<Guid>? KidIds): IRequest<Guid>
    {
        
    }
}
