namespace RequestsMicroservice.MessageBroker
{
    public class ConnectionSettings : IConnectionSettings
    {
        public string Hostname { get; set; }

        public ushort? Port { get; set; }
    }
}