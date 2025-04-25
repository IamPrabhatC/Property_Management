using AutoMapper;
using PropertyManagement.Api.Models.Property;
using PropertyManagement.Application.Dtos;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Api.Mappings
{
    public class AddressMappingProfile : Profile
    {

        public AddressMappingProfile()
        {
            // Map Property -> PropertyResponseDTO
            CreateMap<Address, AddressDto>()
                .ReverseMap();
           
        }
    }  
}
