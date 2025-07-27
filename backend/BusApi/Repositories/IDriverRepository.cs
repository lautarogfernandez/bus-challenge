using BusApi.Domain;
using BusApi.Models;

namespace BusApi.Repositories
{
    public interface IDriverRepository : IRepository<Driver>
    {
        Task<Driver?> GetByIdWithBusAsync(Guid id, CancellationToken cancellationToken);
        Task<List<DriverListResponse>> GetAllListAsync(CancellationToken cancellationToken);
    }
}