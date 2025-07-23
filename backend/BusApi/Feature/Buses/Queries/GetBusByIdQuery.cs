using BusApi.Domain;
using MediatR;

namespace BusApi.Feature.Buses.Queries
{
    public record GetBusByIdQuery(Guid Id) : IRequest<Bus?>
    {
    }
}
