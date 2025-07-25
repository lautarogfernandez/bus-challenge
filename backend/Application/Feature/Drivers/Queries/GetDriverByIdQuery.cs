using BusApi.Models;
using MediatR;

namespace Application.Feature.Drivers.Queries
{
    public record GetDriverByIdQuery(Guid Id) : IRequest<DriverListResponse?>
    {
    }
}
