using BusApi.Data;
using BusApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Repositories
{
    public class BusRepository : Repository<Bus>, IBusRepository
    {
        public BusRepository(ApplicationContext context) : base(context) { }

        public async Task<Bus?> GetByIdWithKidsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Buses
                .Include(b => b.Kids)
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }
    }

}