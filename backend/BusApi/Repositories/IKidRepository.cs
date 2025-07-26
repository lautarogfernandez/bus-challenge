using BusApi.Domain;
using BusApi.Models;

namespace BusApi.Repositories
{
    public interface IKidRepository : IRepository<Kid>
    {
        Task<List<KidListResponse>> GetAllListAsync(CancellationToken cancellationToken);
        Task<Kid?> GetByIdWithBusAsync(Guid id, CancellationToken cancellationToken);
    }
}