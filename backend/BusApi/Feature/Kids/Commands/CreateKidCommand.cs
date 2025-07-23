using MediatR;

namespace BusApi.Feature.Kids.Commands
{
    public record CreateKidCommand(string DocumentNumber, string Name, Guid? BusId): IRequest<Guid>
    {
        
    }
}
