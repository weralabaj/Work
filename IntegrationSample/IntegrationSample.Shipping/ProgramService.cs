using System;
using System.Diagnostics;
using System.ServiceProcess;
using NServiceBus;

class ProgramService : ServiceBase
{
    IBus bus;

    static void Main()
    {
        using (var service = new ProgramService())
        {
            // so we can run interactive from Visual Studio or as a windows service
            if (Environment.UserInteractive)
            {
                service.OnStart(null);
                Console.WriteLine("\r\nPress enter key to stop program\r\n");
                Console.Read();
                service.OnStop();
                return;
            }
            Run(service);
        }
    }

    protected override void OnStart(string[] args)
    {
        var busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("IntegrationSample.Shipping");
        busConfiguration.UseSerialization<XmlSerializer>();

        if (Environment.UserInteractive && Debugger.IsAttached)
        {
            //TODO: For production use, please select a durable persistence.
            busConfiguration.UsePersistence<InMemoryPersistence>();

            //TODO: For production use, please script your installation.
            busConfiguration.EnableInstallers();
            busConfiguration.Conventions()
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace == "IntegrationSample.Messages.Commands");
            busConfiguration.Conventions()
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace == "IntegrationSample.Messages.Events");
        }
        var startableBus = Bus.Create(busConfiguration);
        bus = startableBus.Start();
    }

    protected override void OnStop()
    {
        if (bus != null)
        {
            bus.Dispose();
        }
    }

}