using System;
using NServiceBus.Saga;

namespace IntegrationSample.OrdersProcessor
{
    public class OrderProcessingSagaData : ContainSagaData
    {
        [Unique]
        public Guid OrderId { get; set; }
    }
}
