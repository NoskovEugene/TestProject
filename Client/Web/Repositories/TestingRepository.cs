using Client.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Web.Repositories
{
    public interface ITestingRepository
    {
        bool TestConnectionToServer();
    }

    public class TestingRepository : RepositoryBase, ITestingRepository
    {
        public TestingRepository(IWebClient client, IAppConfig appConfig) : base(client, appConfig)
        {
        }

        public bool TestConnectionToServer()
        {
            return Client.TestConnection("/api/v1/ping");
        }
    }
}