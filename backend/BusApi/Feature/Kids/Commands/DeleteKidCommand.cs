using MediatR;

namespace BusApi.Feature.Kids.Commands
{
    public record DeleteKidCommand(Guid Id) : IRequest<Unit>;
}
