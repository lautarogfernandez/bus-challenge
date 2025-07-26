using BusApi.Domain;

namespace BusApi.Repositories
{
    public interface IBusRepository : IRepository<Bus>
    {
        Task<Bus?> GetByIdWithKidsAsync(Guid id, CancellationToken cancellationToken);
    }
}