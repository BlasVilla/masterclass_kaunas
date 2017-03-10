using System;

namespace Worker.Settings
{
    public class ServiceSettings : IServiceSettings
    {
        public Uri RequestsUrl { get; set; }

        public Uri ResultsUrl { get; set; }
    }
}