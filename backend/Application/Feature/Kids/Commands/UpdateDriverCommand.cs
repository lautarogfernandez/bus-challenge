using MediatR;

namespace Application.Feature.Kids.Commands
{
    public record UpdateKidCommand(Guid Id, string DocumentNumber, string Name) : IRequest<Unit>
    {

    }
}
