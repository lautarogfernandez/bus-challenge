using BusApi.Data;
using BusApi.Domain;
using BusApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Repositories
{
    public class KidRepository : Repository<Kid>, IKidRepository
    {
        public KidRepository(ApplicationContext context) : base(context) { }

        public async Task<Kid?> GetByIdWithBusAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Kids
               .Include(b => b.Bus)
               .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<List<KidListResponse>> GetAllListAsync(CancellationToken cancellationToken)
        {
            return await _context.Kids
                .Select(d => new KidListResponse
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