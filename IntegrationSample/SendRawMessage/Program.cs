using System;
using System.Collections.Generic;
using System.IO;
using System.Messaging;
using System.Text;
using System.Xml.Serialization;
using IntegrationSample.Messages.Commands;

namespace SendRawMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\r\nPress '2' to send raw message\r\n");
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.KeyChar == '2')
                {

                    MessageQueue messageQueue = new MessageQueue(@".\Private$\integrationsample.ordersprocessor");
                    MessageQueueTransaction myTransaction = new
                        MessageQueueTransaction();
                    myTransaction.Begin();
                    var msg = new ProcessOrder
                    {
                        OrderId = Guid.NewGuid(),
                        ProductId = 123456789
                    };
                    var ser = new XmlSerializer(typeof(ProcessOrder));
                    var memoryStream = new MemoryStream();
                    ser.Serialize(memoryStream, msg);

                    memoryStream.Flush();
                    memoryStream.Position = 0;
                    var reader = new StreamReader(memoryStream);
                    var result = reader.ReadToEnd();

                    var message = new Message(msg);
                    //message.Extension =
                    //    Encoding.UTF8.GetBytes("<?xml version=\"1.0\"?><ArrayOfHeaderInfo xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">"
                    //        + "<HeaderInfo><Key>NServiceBus.EnclosedMessageTypes</Key><Value>IntegrationSample.Messages.Commands.ProcessOrder, IntegrationSample.Messages</Value></HeaderInfo>"
                    //    //TODO: add those if you want to have properly functioning SI diagrams
                    //    //+ "<HeaderInfo><Key>NServiceBus.ConversationId</Key><Value>4978b9d8-70da-4b54-9b48-a4eb00e88c92</Value></HeaderInfo>"
                    //    //+ "<HeaderInfo><Key>NServiceBus.OriginatingMachine</Key><Value>SCHMETTERLING</Value></HeaderInfo>"
                    //    //+ "<HeaderInfo><Key>NServiceBus.OriginatingEndpoint</Key><Value>SequenceFun</Value></HeaderInfo>"
                    //  + "</ArrayOfHeaderInfo>");
                    messageQueue.Send(message, myTransaction);
                    myTransaction.Commit();
                }
                Console.WriteLine("\r\nPress '2' to send raw message\r\n");
            }
        }
    }
}
