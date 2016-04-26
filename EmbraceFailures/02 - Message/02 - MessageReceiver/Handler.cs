using System;
using System.IO;
using Messages;
using NServiceBus;

namespace MessageReceiver
{
    class Handler : IHandleMessages<WriteToFile>
    {
        public void Handle(WriteToFile message)
        {
            //File.AppendAllText(@"C:\__WORK\EmbraceFailures\02-message.txt", "Some text saved to a file");
            File.AppendAllText(@"C:\__WORK\EmbraceFailures\1\02-message.txt", "Some text saved to a file");
            Console.WriteLine("Text saved to the file");
        }
    }
}
