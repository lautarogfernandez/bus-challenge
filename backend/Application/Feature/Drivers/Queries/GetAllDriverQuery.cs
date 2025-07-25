using BusApi.Models;
using MediatR;

namespace Application.Feature.Drivers.Queries
{
    public record GetAllDriverQuery() : IRequest<IEnumerable<DriverListResponse>>
    {
    }
}
