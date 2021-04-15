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
        public CartRepository(IWebClient client) : base(client)
        {
        }

        public async Task<int> InitCart()
        {
            return await Client.SendRequestAsync<int>("/api/v1/cart/initCart", HttpMethod.Get).ConfigureAwait(false);
        }
    }
}
