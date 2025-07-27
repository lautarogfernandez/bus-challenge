using MediatR;

namespace BusApi.Feature.Kids.Commands
{
    public record UpdateKidCommand(Guid Id, string DocumentNumber, string Name) : IRequest<Unit>
    {

    }
}
