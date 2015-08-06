using System;
using IntegrationSample.Messages.Events;
using NServiceBus;

namespace IntegrationSample.Shipping
{
    public class OrderShipping : IHandleMessages<OrderProcessed>
    {
        public void Handle(OrderProcessed message)
        {
            Console.WriteLine("Order sent for shipping " + message.OrderId);
        }
    }
}
