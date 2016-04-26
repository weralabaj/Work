using System;
using System.IO;
using System.Messaging;
using NServiceBus;
using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Message
{
    class Program
    {
        static void Main(string[] args)
        {
            BusConfiguration busConfiguration = new BusConfiguration();
            busConfiguration.UsePersistence<InMemoryPersistence>();
            busConfiguration.EnableInstallers();

            using (IBus bus = Bus.Create(busConfiguration).Start())
            {
                while (true)
                {
                    Console.WriteLine("Enter 'C' key to close the app");
                    Console.WriteLine("Enter 'S' key to save the text to the 'text.txt' file");

                    ConsoleKeyInfo key = Console.ReadKey();
                    Console.WriteLine();
                    if (key.Key == ConsoleKey.C)
                    {
                        return;
                    }

                    if (key.Key == ConsoleKey.S)
                    {
                        bus.SendLocal(new WriteToFile());
                    }
                }
            }
        }

        #region config

        class ProvideConfiguration : IProvideConfiguration<MessageForwardingInCaseOfFaultConfig>
        {
            public MessageForwardingInCaseOfFaultConfig GetConfiguration()
            {
                return new MessageForwardingInCaseOfFaultConfig
                {
                    ErrorQueue = "error"
                };
            }
        }

        class ProvideRetriesConfiguration : IProvideConfiguration<SecondLevelRetriesConfig>
        {
            public SecondLevelRetriesConfig GetConfiguration()
            {
                return new SecondLevelRetriesConfig
                {
                    Enabled = true,
                    NumberOfRetries = 1,
                    TimeIncrease = TimeSpan.FromSeconds(10)
                };
            }
        }

        #endregion
    }

    public class Handler : IHandleMessages<WriteToFile>
    {
        public void Handle(WriteToFile message)
        {
            File.AppendAllText(@"C:\__WORK\EmbraceFailures\03-error.txt", "Some text saved to a file");
            //File.AppendAllText(@"C:\__WORK\EmbraceFailures\1\03-error.txt", "Some text saved to a file");
            Console.WriteLine("Text saved to the file");
        }
    }

    public class WriteToFile : ICommand
    {
    }
}
