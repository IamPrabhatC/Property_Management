using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagement.Domain.Services
{
    public class PropertyDomainService : IPropertyDomainService
    {
        public async Task ValidateProductAsync(Property property)
        {
            // Add Domain Validation here
            await Task.CompletedTask;
        }
    }

}
