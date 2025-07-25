using MediatR;

namespace Application.Feature.Kids.Commands
{
    public record CreateKidCommand(string DocumentNumber, string Name, Guid? BusId): IRequest<Guid>
    {
        
    }
}
