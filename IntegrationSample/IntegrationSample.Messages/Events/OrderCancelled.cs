using System;

namespace IntegrationSample.Messages.Events
{
    public class OrderCancelled
    {
        public Guid OrderId { get; set; }
    }
}
