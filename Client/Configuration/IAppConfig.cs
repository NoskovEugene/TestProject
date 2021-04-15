namespace Client.Configuration
{
    public interface IAppConfig
    {
        ColorConfig ColorConfig { get; }

        NetworkConfiguration NetworkConfig { get; }
    }
}