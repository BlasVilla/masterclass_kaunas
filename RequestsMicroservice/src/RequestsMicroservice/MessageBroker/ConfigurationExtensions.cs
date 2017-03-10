using Microsoft.Extensions.Configuration;
using System;

namespace RequestsMicroservice.MessageBroker
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
    }
}