using BusApi.Domain;
using MediatR;

namespace BusApi.Feature.Kids.Queries
{
    public record GetKidByIdQuery(Guid Id) : IRequest<Kid?>
    {
    }
}
