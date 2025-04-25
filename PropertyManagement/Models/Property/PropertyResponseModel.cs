using PropertyManagement.Application.Dtos;

namespace PropertyManagement.Api.Models.Property
{
    public class PropertyResponseModel
    {
        public Guid ExternalId { get; set; }
        public string Name { get; set; }
        public AddressDto Address { get; set; }    
    }
}
