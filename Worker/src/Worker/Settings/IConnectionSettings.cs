namespace Worker.Settings
{
    public interface IConnectionSettings
    {
        string Hostname { get; }

        ushort? Port { get; }
    }
}