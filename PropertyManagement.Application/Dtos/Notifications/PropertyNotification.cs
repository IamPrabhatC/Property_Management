namespace PropertyManagement.Application.Dtos.Notifications
{
    // Message model for Service Bus
    public class PropertyNotification
    {
        public string Event { get; set; }
        public Guid Id { get; set; }
        public string PropertyName { get; set; }     
        public string Address { get; set; }        
    }
}
