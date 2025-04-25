using PropertyManagement.Application.Dtos;

namespace PropertyManagement.Application.Interfaces
    {
        public interface IPropertyService
        {
            /// <summary>
            /// Gets all properties
            /// </summary>
            /// <returns>Collection of property DTOs</returns>
            Task<IEnumerable<PropertyResponseDTO>> GetAllPropertiesAsync();

            /// <summary>
            /// Gets a property by ID
            /// </summary>
            /// <param name="id">Property ID</param>
            /// <returns>Property DTO if found, null otherwise</returns>
            Task<PropertyResponseDTO> GetPropertyByIdAsync(Guid id);

           /// <summary>
           /// Creatte new Property
           /// </summary>
           /// <param name="propertyDto"></param>
           /// <returns></returns>
            Task<PropertyResponseDTO> CreatePropertyAsync(PropertyRequestDTO propertyDto);

            /// <summary>
            /// Update existing Property
            /// </summary>
            /// <param name="id"></param>
            /// <param name="propertyDto"></param>
            /// <returns></returns>
            Task<PropertyResponseDTO> UpdatePropertyAsync(Guid id, PropertyRequestDTO propertyDto);

            /// <summary>
            /// Deletes a property by ID
            /// </summary>
            /// <param name="id">Property ID to delete</param>
            /// <returns>True if deleted, false if not found</returns>
            Task<bool> DeletePropertyAsync(Guid id);
        }
    }
