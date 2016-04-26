using System;
using System.Collections.Generic;

namespace IntegrationSample.Messages.Commands
{
    public class ProcessOrder
    {
        public Guid OrderId { get; set; }
        public int  ProductId { get; set; }
    }
}
