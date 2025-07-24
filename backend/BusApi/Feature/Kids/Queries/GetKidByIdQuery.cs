using BusApi.Models;
using MediatR;

namespace BusApi.Feature.Kids.Queries
{
    public record GetKidByIdQuery(Guid Id) : IRequest<KidListResponse?>
    {
    }
}
