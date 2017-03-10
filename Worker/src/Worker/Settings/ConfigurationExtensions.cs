using Microsoft.Extensions.Configuration;
using System;

namespace Worker.Settings
{
    public static class ConfigurationExtensions
    {
        public static IConnectionSettings GetConnectionSettings(this IConfigurationRoot configuration)
        {
            return new ConnectionSettings
            {
                Hostname = configuration["MessageBroker:Hostname"],
                Port = configuration.GetValue<ushort?>("MessageBroker:Port")
            };
        }

        public static IServiceSettings GetServiceSettings(this IConfigurationRoot configuration)
        {
            return new ServiceSettings
            {
                RequestsUrl = configuration.GetValue<Uri>("Services:RequestsUrl"),
                ResultsUrl = configuration.GetValue<Uri>("Services:ResultsUrl")
            };
        }

        public static ISimulationSettings GetSimulationSettings(this IConfigurationRoot configuration)
        {
            return new SimulationSettings
            {
                DelayInMilliseconds = configuration.GetValue<int>("Simulation:Delay")
            };
        }
    }
}