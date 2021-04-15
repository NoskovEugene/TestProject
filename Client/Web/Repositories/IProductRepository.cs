using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dtos;

namespace Client.Web.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}