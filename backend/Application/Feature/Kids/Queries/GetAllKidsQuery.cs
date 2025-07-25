using BusApi.Models;
using MediatR;

namespace Application.Feature.Kids.Queries
{
    public record GetAllKidsQuery() : IRequest<IEnumerable<KidListResponse>>
    {
    }
}
