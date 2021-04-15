using System.Net.Http;
using System.Threading.Tasks;
using Common.RequestModels;

namespace Client.Web
{
    public interface IWebClient
    {
        Task<T> SendRequestAsync<T>(string url, HttpMethod? method, bool isRelative = true);

        T SendRequest<T>(string url, HttpMethod? method, bool isRelative = true);
    }
}