using Client.Configuration;
using Common.Dtos;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.Repositories
{
    public class CartRepository : RepositoryBase, ICartRepository
    {
        public CartRepository(IWebClient client, IAppConfig appConfig) : base(client, appConfig)
        {
        }

        public async Task<ProductCartDto> InitCart()
        {
            var config = new RequestBuilder().Url("/api/v1/cart/initCart")
                .IsRelative(Network.ServerUrl)
                .Method(HttpMethod.Get)
                .Build();
            return await Client.SendRequestAsync<ProductCartDto>(config).ConfigureAwait(false);
        }
    }
}
