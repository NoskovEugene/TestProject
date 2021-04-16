using Client.Configuration;

namespace Client.Web.Repositories
{
    public class RepositoryBase
    {
        public IWebClient Client { get; }

        public NetworkConfiguration Network { get; }

        public RepositoryBase(IWebClient client, IAppConfig appConfig)
        {
            Client = client;
            Network = appConfig.NetworkConfig;
        }
    }
}