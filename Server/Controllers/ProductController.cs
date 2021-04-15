using System;
using System.Collections.Generic;
using System.Linq;
using Common.Models;
using Common.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Server.DAL.Repositories;

namespace Server.Controllers
{
    [Route("/api/v1/product/[action]")]
    public class ProductController : Controller
    {
        
        protected IRepositoryBase<Product> ProductRepository { get; }
        
        public ProductController(IRepositoryBase<Product> productRepository)
        {
            ProductRepository = productRepository;
        }

        
        [ActionName("getAllProducts")]
        [HttpPost]
        public IEnumerable<Product> GetProducts()
        {
            return ProductRepository.Query(x => x.Id != 0).ToList();
        }
        
        [ActionName("addNewProduct")]
        [HttpPost]
        public SuccessResponse<string> AddNewProduct(Product product)
        {
            try
            {
                ProductRepository.Insert(product);
                return new SuccessResponse<string>()
                {
                    Success = true,
                    Payload = "Продукт успешно добавлен"
                };
            }
            catch (Exception e)
            {
                return new SuccessResponse<string>()
                {
                    Success = false,
                    Payload = e.Message
                };
            }
        }
        
        [ActionName("removeProduct")]
        [HttpPost]
        public SuccessResponse<string> RemoveProduct([FromBody]int id)
        {
            ProductRepository.Remove(id);
            return new SuccessResponse<string>()
            {
                Success = true,
                Payload = "Продукт успешно удалён"
            };
        }
    }
}