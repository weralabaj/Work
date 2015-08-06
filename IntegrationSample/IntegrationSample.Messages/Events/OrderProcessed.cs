using System;

namespace IntegrationSample.Messages.Events
{
    public class OrderProcessed
    {
        public Guid OrderId { get; set; }
    }
}
