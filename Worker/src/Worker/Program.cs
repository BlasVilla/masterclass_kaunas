using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Worker.DI;
using Worker.Messaging;
using Worker.Settings;

namespace Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();

            var connectionSettings = configuration.GetConnectionSettings();

            var serviceSettings = configuration.GetServiceSettings();

            var simulationSettings = configuration.GetSimulationSettings();
            
            var services = CreateServiceProvider(connectionSettings, serviceSettings, simulationSettings);

            var messageService = services.GetService<IMessageService>();

            messageService.Run();
        }
        
        protected static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json");

            return builder.Build();
        }

        protected static IServiceProvider CreateServiceProvider(IConnectionSettings connectionSettings, 
            IServiceSettings serviceSettings, ISimulationSettings simulationSettings)
        {
            var serviceCollection = new ServiceCollection();
            
            serviceCollection
                .AddAllDependencies()
                .AddSingleton(connectionSettings)
                .AddSingleton(serviceSettings)
                .AddSingleton(simulationSettings);

            return serviceCollection.BuildServiceProvider();
        }
    }
}