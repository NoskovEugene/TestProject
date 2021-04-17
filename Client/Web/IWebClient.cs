using System.Net.Http;
using System.Threading.Tasks;
using Common.RequestModels;

namespace Client.Web
{
    public interface IWebClient
    {
        Task<T> SendRequestAsync<T>(HttpRequestMessage config);

        bool TestConnection(string url, bool isRelative = true);

        T SendRequest<T>(HttpRequestMessage config);

        Task DownloadFile(HttpRequestMessage config, string pathToFile);
    }
}