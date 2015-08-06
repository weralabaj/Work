using System;
using System.Collections.Generic;

namespace IntegrationSample.Messages.Commands
{
    public class ProcessOrder
    {
        public Guid OrderId { get; set; }
        public List<string> Items { get; set; }
    }
}
