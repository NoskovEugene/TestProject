using System.Threading.Tasks;

namespace Client.Web.Repositories
{
    public interface ICartRepository
    {
        Task<int> InitCart();
    }
}