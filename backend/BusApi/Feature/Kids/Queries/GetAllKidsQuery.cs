using BusApi.Models;
using MediatR;

namespace BusApi.Feature.Kids.Queries
{
    public record GetAllKidsQuery() : IRequest<IEnumerable<KidListResponse>>
    {
    }
}
