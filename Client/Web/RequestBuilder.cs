using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Newtonsoft.Json;

namespace Client.Web
{
    public interface IRequestBuilder
    {
        IRequestBuilder AddQueryParam(string name, object value);
        IRequestBuilder AddQueryParam<T>(Expression<Func<T>> memberExpression);
        IRequestBuilder Url(string url);
        IRequestBuilder IsRelative(string host);
        IRequestBuilder Method(HttpMethod method);
        IRequestBuilder Content(object content);
        HttpRequestMessage Build();
    }

    public class RequestBuilder : IRequestBuilder
    {
        private string url;
        private HttpMethod method;
        private object content;

        public RequestBuilder()
        {
        }

        public IRequestBuilder Url(string url)
        {
            this.url = url;
            return this;
        }

        public IRequestBuilder IsRelative(string host)
        {
            url = Flurl.Url.Combine(host, url);
            return this;
        }

        public IRequestBuilder AddQueryParam(string name, object value)
        {
            url = url.SetQueryParam(name, value);
            return this;
        }

        public IRequestBuilder AddQueryParam<T>(Expression<Func<T>> memberExpression)
        {
            var expressionBody = (MemberExpression)memberExpression.Body;
            var value = memberExpression.Compile().Invoke();
            var name = expressionBody.Member.Name;
            return AddQueryParam(name, value);
        }

        public IRequestBuilder Method(HttpMethod method)
        {
            this.method = method;
            return this;
        }

        public IRequestBuilder Content(object content)
        {
            this.content = content;
            return this;
        }

        public HttpRequestMessage Build()
        {
            var config = new HttpRequestMessage(method, url);
            if (content != null)
            {
                var json = JsonConvert.SerializeObject(content);
                config.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            return config;
        }
    }
}
