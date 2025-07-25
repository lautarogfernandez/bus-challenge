using BusApi.Models;
using MediatR;

namespace Application.Feature.Kids.Queries
{
    public record GetKidByIdQuery(Guid Id) : IRequest<KidListResponse?>
    {
    }
}
