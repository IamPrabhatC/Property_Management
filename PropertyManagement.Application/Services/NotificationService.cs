using Azure.Messaging.ServiceBus;
using PropertyManagement.Application.Dtos.Notifications;
using PropertyManagement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using PropertyManagement.Application.Interfaces;

namespace PropertyManagement.Application.Services
{
    /// <summary>
    /// Notification Service is used to queue messages to azure service bus.
    /// </summary>
    public class NotificationService: INotificationService
    {
        private readonly ServiceBusClient serviceBusClient;
        private readonly string queueOrTopicName;
        private readonly ILogger<NotificationService> logger;

        public NotificationService(
            ServiceBusClient serviceBusClient,
            ILogger<NotificationService> logger,
            IConfiguration configuration)
        {
            this.serviceBusClient = serviceBusClient;
            this.logger = logger;
            queueOrTopicName = configuration["ServiceBus:QueueName"];
        }

        public async Task SendPropertyCreatedNotificationAsync(PropertyResponseDTO property)
        {
            try
            {
                // Create a notification message
                var notification = new PropertyNotification
                {
                    Event = "PropertyCreated",
                    Id = property.ExternalId,
                    PropertyName = property.Name,                    
                    Address = $"{property.Address.Line1} {property.Address.Line2} {property.Address.City} {property.Address.State} {property.Address.ZipCode} ",                    
                };

                // Create a sender for the queue or topic
                ServiceBusSender sender = serviceBusClient.CreateSender(queueOrTopicName);

                // Serialize the notification to JSON
                string messageBody = JsonSerializer.Serialize(notification);

                // Create a message
                ServiceBusMessage message = new ServiceBusMessage(messageBody)
                {
                    ContentType = "application/json",
                    Subject = "PropertyCreated"
                };

                // Send the message
                await sender.SendMessageAsync(message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send property created notification");                
            }
        }
    }
}
