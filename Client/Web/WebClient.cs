using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Client.Configuration;
using System.IO;
using Flurl;
using Common.RequestModels;
using System;
using System.Net;

namespace Client.Web
{
    public class WebClient : IWebClient
    {
        protected HttpClient Client { get; set; }

        protected IAppConfig AppConfig { get; set; }

        public bool ServerAvailable { get; set; }

        public WebClient(IAppConfig appConfig)
        {
            Client = new HttpClient();
            AppConfig = appConfig;
        }


        public async Task<T> SendRequestAsync<T>(string url, HttpMethod? method, bool isRelative = true)
        {
            var config = CreateMessage(url, method, isRelative);
            var response = await Client.SendAsync(config).ConfigureAwait(false);
            var obj = await DeserializeObject<T>(response);
            return obj;
        }

        public T SendRequest<T>(string url, HttpMethod? method, bool isRelative = true)
        {
            var config = CreateMessage(url, method, isRelative);
            var response = Client.Send(config);
            var obj = DeserializeObject<T>(response).Result;
            return obj;
        }

        public bool TestConnection(string url, bool isRelative = true)
        {
            url = isRelative ? Url.Combine(AppConfig.NetworkConfig.ServerUrl, url) : url;
            var request = WebRequest.Create(url);
            request.Timeout = 700;
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                response.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private HttpRequestMessage CreateMessage(string url, HttpMethod? method, bool isRelative = true)
        {
            method ??= HttpMethod.Post;
            url = isRelative ? Url.Combine(AppConfig.NetworkConfig.ServerUrl, url) : url;
            var config = new HttpRequestMessage(method, url);
            return config;
        }
        
        
        private async Task<T> DeserializeObject<T>(HttpResponseMessage message)
        {
            var jsonText = await message.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<T>(jsonText);
            return obj;
        }
    }
}
