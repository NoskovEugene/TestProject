using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Common.Dtos;
using Common.Models;
using Common.RequestModels;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DAL.Repositories;

namespace Server.Controllers
{
    [Route("api/v1/cart/[action]")]
    public class CartController : Controller
    {
        protected IRepositoryBase<ProductCart> CartRepository { get; }

        protected IRepositoryBase<Product> ProductRepository { get; }

        protected IMapper Mapper { get; }

        public CartController(IRepositoryBase<ProductCart> cartRepository,
                              IRepositoryBase<Product> productRepository,
                              IMapper mapper)
        {
            CartRepository = cartRepository;
            ProductRepository = productRepository;
            Mapper = mapper;
        }

        [ActionName("initCart")]
        [HttpPost]
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
        public SuccessResponse<CartItemDto> AddProduct([FromQuery] int productId,
                                                  [FromQuery] int cartId,
                                                  [FromQuery] int enteredDiscount)
        {
            var product = ProductRepository.Query(x => x.Id == productId).First();
            var productDto = Mapper.Map<ProductDto>(product);
            if (enteredDiscount > productDto.MaxDiscount)
            {
                enteredDiscount = 0;
            }

            var finalPrice = productDto.Price - productDto.Price / (double) 100 * (double) enteredDiscount;

            var cartItem = new CartItem()
            {
                Product = product,
                EnteredDiscount = enteredDiscount,
                FinalPrice = finalPrice
            };

            var cart = CartRepository.Query(x => x.Id == cartId).First();
            cart.CartItems.Add(cartItem);
            cart.TotalSum += productDto.Price;
            cart.TotalSumWithDiscount += cartItem.FinalPrice;
            CartRepository.Update(cart);

            return new()
            {
                Success = true,
                Payload = Mapper.Map<CartItemDto>(cartItem)
            };
        }

        [ActionName("removeCartItem")]
        [HttpPost]
        public SuccessResponse<ProductCartDto> RemoveCartItem([FromQuery]int cartId, [FromQuery]int cartItemId)
        {
            var cart = CartRepository.Query(x => x.Id == cartId)
                                     .Include(x=> x.CartItems)
                                     .First();
            var cartDto = Mapper.Map<ProductCartDto>(cart);
            if (cart != null)
            {
                var item = cartDto.CartItems.Where(x => x.Id == cartItemId).First();
                if (item != null)
                {
                    var cartItem = Mapper.Map<CartItem>(item);
                    cart.TotalSum -= item.Product.Price;
                    cart.TotalSumWithDiscount -= item.FinalPrice;
                    cart.CartItems.Remove(cartItem);
                    CartRepository.Update(cart);
                    return new SuccessResponse<ProductCartDto>()
                    {
                        Success = true,
                        Payload = Mapper.Map<ProductCartDto>(cart)
                    };
                }
                else
                {
                    return new SuccessResponse<ProductCartDto>()
                    {
                        Success = false
                    };
                }
            }
            return new SuccessResponse<ProductCartDto>()
            {
                Success = false 
            };
        }

        [ActionName("getReceipt")]
        [HttpGet]
        public IActionResult GetReceipt([FromQuery]int cartId)
        {
            var cart = CartRepository.Query(x => x.Id == cartId).First();
            var pathToCart = CreateReceipt(Mapper.Map<ProductCartDto>(cart));
            var bytes = System.IO.File.ReadAllBytes(pathToCart);
            return File(bytes, "application/text");
        }
        
        [ActionName("getCart")]
        [HttpPost]
        public SuccessResponse<ProductCartDto> GetCart([FromQuery] int cartId)
        {
            var query = CartRepository.Query(x => x.Id == cartId)
                                      .Include(x => x.CartItems)
                                      .ThenInclude(x=> x.Product)
                                      .FirstOrDefault();
            var mapped = Mapper.Map<ProductCart, ProductCartDto>(query);
            if (query != null)
            {
                return new SuccessResponse<ProductCartDto>()
                {
                    Success = true,
                    Payload = Mapper.Map<ProductCartDto>(query)
                };
            }
            else
            {
                return new SuccessResponse<ProductCartDto>()
                {
                    Success = false
                };
            }
        }

        private string CreateReceipt(ProductCartDto cart)
        {
            var storage = Path.Combine(Directory.GetCurrentDirectory(), "storage");
            if (!Directory.Exists(storage))
            {
                Directory.CreateDirectory(storage);
            }
            var receiptFile = Path.Combine(storage, $"receipt{cart.Id}.txt");
            if (System.IO.File.Exists(receiptFile))
            {
                System.IO.File.Delete(receiptFile);
            }
            var stream = new FileStream(receiptFile, FileMode.Create, FileAccess.ReadWrite);
            using var writer = new StreamWriter(stream);
            writer.WriteLine($"{new string(' ', 50)}Чек №{cart.Id}");
            writer.WriteLine(new string('-', 110));
            writer.WriteLine(string.Format("{0,-30} {1,-30} {2,-20:P1} {3,-30}", "Название продукта", "Цена продукта без скидки", "Скидка", "Цена продукта со скидкой"));
            writer.WriteLine(new string('-', 110));
            foreach (var cartItem in cart.CartItems)
            {
                writer.WriteLine($"{cartItem.Product.Name,-30} {cartItem.Product.Price,-30:C2} {(cartItem.EnteredDiscount / 100),-20:P} {cartItem.FinalPrice,-30:C2}");
            }
            writer.WriteLine(new string('-', 110));
            writer.WriteLine($"Цена чека без скидки: {cart.TotalSum:C2}");
            writer.WriteLine($"Цена чека со скидкой: {cart.TotalSumWithDiscount:C2}");

            return receiptFile;
        }
        
    }
}