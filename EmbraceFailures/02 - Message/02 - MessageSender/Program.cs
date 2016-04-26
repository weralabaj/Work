using System;
using Messages;
using NServiceBus;
using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace MessageSender
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
                        bus.Send(new WriteToFile());
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
}
