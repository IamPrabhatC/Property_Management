using AutoMapper;
using PropertyManagement.Application.Common;
using PropertyManagement.Application.Dtos;
using PropertyManagement.Application.Interfaces;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;

namespace PropertyManagement.Application.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPropertyDomainService propertyDomainService;
        private readonly IMapper mapper;
        private readonly INotificationService notificationService;

        public PropertyService(
            IUnitOfWork unitOfWork,
            IPropertyDomainService propertyDomainService,
            IMapper mapper,
            INotificationService notificationService)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));            
            this.propertyDomainService = propertyDomainService ?? throw new ArgumentNullException(nameof(propertyDomainService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        public async Task<IEnumerable<PropertyResponseDTO>> GetAllPropertiesAsync()
        {
            var properties = await unitOfWork.PropertyRepository.GetAllAsync();
            return mapper.Map<IEnumerable<PropertyResponseDTO>>(properties);
        }

        public async Task<PropertyResponseDTO?> GetPropertyByIdAsync(Guid id)
        {
            var property = await unitOfWork.PropertyRepository.GetByIdAsync(id);
            return property != null ? mapper.Map<PropertyResponseDTO>(property) : null;
        }

        public async Task<PropertyResponseDTO> CreatePropertyAsync(PropertyRequestDTO propertyDto)
        {
            // Map DTO to domain entity
            var property = mapper.Map<Property>(propertyDto);

            // Validate property using domain service
            // TODO: Call Validation

            // Add property to repository
            await unitOfWork.PropertyRepository.AddAsync(property);

            // Save changes
            await unitOfWork.CommitAsync();

            var response = mapper.Map<PropertyResponseDTO>(property);
            await notificationService.SendPropertyCreatedNotificationAsync(response); // Send notifcation
            
            return response;
        }

        public async Task<PropertyResponseDTO> UpdatePropertyAsync(Guid id, PropertyRequestDTO propertyDto)
        {
            // Check if property exists
            var existingProperty = await unitOfWork.PropertyRepository.GetByIdAsync(id);
            if (existingProperty == null)
            {
                throw new NotFoundException(nameof(id), id);
            }

            // Map updates from DTO to existing entity
            mapper.Map(propertyDto, existingProperty);

            // Validate updated property
            //TODO: Call Validation

            // Update property
            unitOfWork.PropertyRepository.Update(existingProperty);

            // Save changes
            await unitOfWork.CommitAsync();

            // Return mapped response
            return mapper.Map<PropertyResponseDTO>(existingProperty);
        }

        public async Task<bool> DeletePropertyAsync(Guid id)
        {
            // Check if property exists
            var property = await unitOfWork.PropertyRepository.GetByIdAsync(id);
            if (property == null)
            {
                throw new NotFoundException(nameof(id), id);
            }

            // Delete property
            unitOfWork.PropertyRepository.Delete(property);

            // Save changes
            await unitOfWork.CommitAsync();

            return true;
        }
    }
}