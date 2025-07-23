using BusApi.Domain;
using MediatR;

namespace BusApi.Feature.Drivers.Queries
{
    public record GetDriverByIdQuery(Guid Id) : IRequest<Driver?>
    {
    }
}
