using BusApi.Domain;
using BusApi.Models;

namespace BusApi.Repositories
{
    public interface IBusRepository : IRepository<Bus>
    {
        Task<Bus?> GetByIdWithKidsAsync(Guid id, CancellationToken cancellationToken);
        Task<List<BusListResponse>> GetAllListAsync(CancellationToken cancellationToken);
    }
}