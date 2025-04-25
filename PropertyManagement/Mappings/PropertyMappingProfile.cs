using AutoMapper;
using PropertyManagement.Api.Models.Property;
using PropertyManagement.Application.Dtos;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Api.Mappings
{
    public class PropertyMappingProfile : Profile
    {
        public PropertyMappingProfile()
        {
            // Map Property -> PropertyResponseDTO
            CreateMap<Property, PropertyResponseDTO>();

            // Map PropertyRequestDTO -> Property
            CreateMap<PropertyRequestDTO, Property>();

            // Allow updates from PropertyRequestDTO to Property
            CreateMap<PropertyRequestDTO, Property>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Map from PropertyRequestModel to PropertyRequestDTO
            CreateMap<PropertyRequestModel, PropertyRequestDTO>()
                .ReverseMap(); 

            // Map from PropertyResponseDTO to PropertyResponseModel
            CreateMap<PropertyResponseDTO, PropertyResponseModel>()
             .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
             .ReverseMap(); 
        }
    }
}
