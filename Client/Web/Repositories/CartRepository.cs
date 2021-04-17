using Client.Configuration;
using Common.Dtos;
using Common.Models;
using Common.RequestModels;
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
                .Method(HttpMethod.Post)
                .Build();
            return await Client.SendRequestAsync<ProductCartDto>(config).ConfigureAwait(false);
        }

        public async Task<SuccessResponse<CartItemDto>> AddProduct(int productId, int cartId, double enteredDiscount)
        {
            var config = new RequestBuilder().Url("/api/v1/cart/addProduct")
                .IsRelative(Network.ServerUrl)
                .Method(HttpMethod.Post)
                .AddQueryParam("productId", productId)
                .AddQueryParam("cartId", cartId)
                .AddQueryParam("enteredDiscount", enteredDiscount)
                .Build();
            return await Client.SendRequestAsync<SuccessResponse<CartItemDto>>(config).ConfigureAwait(false);
        }

        public async Task<SuccessResponse<ProductCartDto>> RemoveCartItem(int cartId, int cartItemId)
        {
            var config = new RequestBuilder().Url("/api/v1/cart/removeCartItem")
                .IsRelative(Network.ServerUrl)
                .Method(HttpMethod.Post)
                .AddQueryParam(() => cartId)
                .AddQueryParam(() => cartItemId)
                .Build();

            return await Client.SendRequestAsync<SuccessResponse<ProductCartDto>>(config).ConfigureAwait(false);
        }

        public async Task<SuccessResponse<ProductCartDto>> GetCart(int cartId)
        {
            var config = new RequestBuilder()
                .Url("/api/v1/cart/getCart")
                .IsRelative(Network.ServerUrl)
                .Method(HttpMethod.Post)
                .AddQueryParam(() => cartId)
                .Build();
            return await Client.SendRequestAsync<SuccessResponse<ProductCartDto>>(config).ConfigureAwait(false);
        }

        public async Task GetReceipt(int cartId, string pathToFile)
        {
            var config = new RequestBuilder()
                .Url("/api/v1/cart/getReceipt")
                .IsRelative(Network.ServerUrl)
                .Method(HttpMethod.Get)
                .AddQueryParam(() => cartId)
                .Build();
            await Client.DownloadFile(config, pathToFile);
        }
    }
}
