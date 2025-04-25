using PropertyManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagement.Domain.Interfaces
{
    public interface IPropertyDomainService
    {
        Task ValidateProductAsync(Property property);
 
    }

}
