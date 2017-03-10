namespace Worker.Settings
{
    public class ConnectionSettings : IConnectionSettings
    {
        public string Hostname { get; set; }

        public ushort? Port { get; set; }
    }
}