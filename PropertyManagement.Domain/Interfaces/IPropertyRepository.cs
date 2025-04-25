using PropertyManagement.Domain.Entities;



namespace PropertyManagement.Domain.Interfaces
    {
        public interface IPropertyRepository : IRepositoryBase<Property>
        {
            Task<Property?> GetByIdAsync(Guid id);
            Task<IEnumerable<Property>> GetAllAsync();            
        }
    }


