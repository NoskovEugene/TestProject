using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Dtos;

namespace Client.Web.Repositories
{

    public class ProductRepository : RepositoryBase, IProductRepository
    {


        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            return await Client.SendRequestAsync<IEnumerable<ProductDto>>("/api/v1/product/getAllProducts", HttpMethod.Post).ConfigureAwait(false);
        }

        public ProductRepository(IWebClient client) : base(client)
        {
        }
    }
}