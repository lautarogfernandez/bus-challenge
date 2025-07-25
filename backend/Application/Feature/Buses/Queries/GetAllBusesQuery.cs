using BusApi.Models;
using MediatR;

namespace Application.Feature.Buses.Queries
{
    public record GetAllBusesQuery() : IRequest<IEnumerable<BusListResponse>>
    {
    }
}
