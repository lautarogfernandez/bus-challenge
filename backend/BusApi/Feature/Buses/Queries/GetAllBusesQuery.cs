using BusApi.Models;
using MediatR;

namespace BusApi.Feature.Buses.Queries
{
    public record GetAllBusesQuery() : IRequest<IEnumerable<BusListResponse>>
    {
    }
}
