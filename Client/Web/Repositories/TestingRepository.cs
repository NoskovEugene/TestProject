using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Web.Repositories
{
    public interface ITestingRepository
    {
        Task<bool> TestConnectionToServer();
    }

    public class TestingRepository : RepositoryBase, ITestingRepository
    {
        public TestingRepository(IWebClient client) : base(client)
        {
        }

        public async Task<bool> TestConnectionToServer()
        {
            try
            {
                return await Client.SendRequestAsync<bool>(
                    "/api/v1/product/getAllProducts", HttpMethod.Post).ConfigureAwait(false);
            }
            catch
            {
                return false;
            }

        }
    }
}