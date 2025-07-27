using BusApi.Models;
using MediatR;

namespace BusApi.Feature.Drivers.Queries
{
    public record GetAllDriverQuery() : IRequest<IEnumerable<DriverListResponse>>
    {
    }
}
