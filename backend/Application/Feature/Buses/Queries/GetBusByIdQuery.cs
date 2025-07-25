using BusApi.Models;
using MediatR;

namespace Application.Feature.Buses.Queries
{
    public record GetBusByIdQuery(Guid Id) : IRequest<BusResponse?>
    {
    }
}
