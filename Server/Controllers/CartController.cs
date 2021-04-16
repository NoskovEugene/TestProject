using System.Linq;
using AutoMapper;
using Common.Dtos;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Server.DAL.Repositories;

namespace Server.Controllers
{
    [Route("api/v1/cart/[action]")]
    public class CartController : Controller
    {
        protected IRepositoryBase<ProductCart> CartRepository { get; set; }

        protected  IMapper Mapper { get; }
        
        public CartController(IRepositoryBase<ProductCart> cartRepository,
                              IMapper mapper)
        {
            CartRepository = cartRepository;
            Mapper = mapper;
        }

        [ActionName("initCart")]
        [HttpGet]
        public ProductCartDto InitCart()
        {
            var id = CartRepository.Query(x => x.Id >= 0).Max(x => x.Id) + 1;
            var cart = new ProductCart()
            {
                Id = id
            };
            CartRepository.Insert(cart);
            return Mapper.Map<ProductCartDto>(cart);
        }

        [ActionName("addProduct")]
        [HttpPost]
        public void AddProduct([FromBody]Product product, [FromQuery]int id)
        {
            
        }
    }
}