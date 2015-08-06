using System;
using IntegrationSample.Messages.Commands;
using IntegrationSample.Messages.Events;
using NServiceBus.Saga;

namespace IntegrationSample.OrdersProcessor
{
    public class OrderProcessingSaga : Saga<OrderProcessingSagaData>,
        IAmStartedByMessages<ProcessOrder>,
        IHandleTimeouts<OrderCancellationTimeout>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderProcessingSagaData> mapper)
        {
            mapper.ConfigureMapping<OrderCancellationTimeout>(m => m.OrderId).ToSaga(s => s.OrderId);
        }

        public void Handle(ProcessOrder message)
        {
            this.Data.OrderId = message.OrderId;
            Console.WriteLine("Processing order " + this.Data.OrderId);
            this.RequestTimeout<OrderCancellationTimeout>(TimeSpan.FromSeconds(5));
        }

        public void Timeout(OrderCancellationTimeout state)
        {
            this.Bus.Publish<OrderProcessed>(e => e.OrderId = this.Data.OrderId);
            Console.WriteLine("Order processed " + this.Data.OrderId);
            this.MarkAsComplete();
        }
    }
}
