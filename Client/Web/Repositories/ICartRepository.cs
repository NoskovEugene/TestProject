using Common.Dtos;
using Common.Models;
using Common.RequestModels;
using System.Threading.Tasks;

namespace Client.Web.Repositories
{
    public interface ICartRepository
    {
        Task<ProductCartDto> InitCart();

        Task<SuccessResponse<CartItemDto>> AddProduct(int productId, int cartId, double enteredDiscount);

        Task<SuccessResponse<ProductCartDto>> RemoveCartItem(int cartId, int cartItemId);
    }
}