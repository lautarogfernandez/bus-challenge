using MediatR;

namespace Application.Feature.Kids.Commands
{
    public record DeleteKidCommand(Guid Id) : IRequest<Unit>;
}
