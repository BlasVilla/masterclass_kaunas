namespace RequestsMicroservice.MessageBroker
{
    public interface IConnectionSettings
    {
        string Hostname { get; }

        ushort? Port { get; }
    }
}