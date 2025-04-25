using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagement.Application.Dtos
{
    public class PropertyResponseDTO
    {
        public Guid ExternalId { get; set; }
        public string Name { get; set; }   
        public AddressDto Address { get; set; }
    }
}
