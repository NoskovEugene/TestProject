using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Client.Configuration;
using Common.Dtos;

namespace Client.Web.Repositories
{

    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(IWebClient client, IAppConfig appConfig) : base(client, appConfig)
        {
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var config = new RequestBuilder().Url("/api/v1/product/getAllProducts")
                .IsRelative(Network.ServerUrl)
                .Method(HttpMethod.Post)
                .Build();
            return await Client.SendRequestAsync<IEnumerable<ProductDto>>(config).ConfigureAwait(false);
        }
    }
}