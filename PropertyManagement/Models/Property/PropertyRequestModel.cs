using PropertyManagement.Application.Dtos;

namespace PropertyManagement.Api.Models.Property
{
    public class PropertyRequestModel
    {
        public string Name { get; set; }      
        public AddressDto Address { get; set; }
        
    }
}
