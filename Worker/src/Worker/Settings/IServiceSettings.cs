using System;

namespace Worker.Settings
{
    public interface IServiceSettings
    {
        Uri RequestsUrl { get; }

        Uri ResultsUrl { get; }
    }
}