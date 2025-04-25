using PropertyManagement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagement.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendPropertyCreatedNotificationAsync(PropertyResponseDTO property);
    }
}
