using Common.Dtos;
using Common.Models;
using System.Threading.Tasks;

namespace Client.Web.Repositories
{
    public interface ICartRepository
    {
        Task<ProductCartDto> InitCart();
    }
}