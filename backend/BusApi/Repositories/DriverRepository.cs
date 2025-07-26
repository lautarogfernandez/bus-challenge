using BusApi.Data;
using BusApi.Domain;
using BusApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Repositories
{
    public class DriverRepository : Repository<Driver>, IDriverRepository
    {
        public DriverRepository(ApplicationContext context) : base(context) { }

        public async Task<Driver?> GetByIdWithBusAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Drivers
               .Include(b => b.Bus)
               .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<List<DriverListResponse>> GetAllListAsync(CancellationToken cancellationToken)
        {
            return await _context.Drivers
                .Include(d => d.Bus)
                .Select(d => new DriverListResponse
                {
                    Id = d.Id,
                    Name = d.Name,
                    DocumentNumber = d.DocumentNumber,
                    BusRegistrationPlate = d.Bus != null ? d.Bus.RegistrationPlate : null
                })
                .ToListAsync(cancellationToken);
        }
    }
}