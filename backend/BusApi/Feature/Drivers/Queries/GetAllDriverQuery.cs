using BusApi.Domain;
using MediatR;

namespace BusApi.Feature.Drivers.Queries
{
    public record GetAllDriverQuery() : IRequest<IEnumerable<Driver>>
    {
    }
}
