using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagement.Application.Dtos
{
    public class PropertyRequestDTO
    {
        public string Name { get; set; }        
        public AddressDto Address { get; set; }
    }
}
