using PropertyManagement.Domain.Interfaces;
using PropertyManagement.Infrastructure.Persistence;

namespace PropertyManagement.Application.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private IPropertyRepository propertyRepository;

        public UnitOfWork(ApplicationDbContext context, IPropertyRepository propertyRepository)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.propertyRepository = propertyRepository ?? throw new ArgumentNullException(nameof(propertyRepository));
        }

        // Expose the PropertyRepository
        public IPropertyRepository PropertyRepository => propertyRepository;

        // Save changes to the database
        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }

        // Dispose the context
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
