using Microsoft.EntityFrameworkCore;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;
using PropertyManagement.Infrastructure.Persistence;

namespace PropertyManagement.Infrastructure.Repositories
{
    public class PropertyRepository : RepositoryBase<Property>, IPropertyRepository
    {
        public PropertyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _dbSet.Include(p => p.Address)             
                .ToListAsync();
        }
        public async Task<Property?> GetByIdAsync(Guid id)
        {
            return await _dbSet.Include(p=>p.Address).FirstOrDefaultAsync(p => p.ExternalId == id);
        }

    }
}
