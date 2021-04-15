using System.Linq;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Server.DAL.Repositories;

namespace Server.Controllers
{
    [Route("api/v1/cart/[action]")]
    public class CartController : Controller
    {
        protected IRepositoryBase<ProductCart> CartRepository { get; set; }
        
        public CartController(IRepositoryBase<ProductCart> cartRepository)
        {
            CartRepository = cartRepository;
        }
        
        [ActionName("initCart")]
        [HttpGet]
        public int InitCart()
        {
            var query = CartRepository.Query(x => x.Id >= 0).Max(x=> x.Id) + 1;
            return query;
        }

        [ActionName("addCart")]
        [HttpPost]
        public void InsertCart(ProductCart cart)
        {
            CartRepository.Insert(cart);
        }
        
    }
}