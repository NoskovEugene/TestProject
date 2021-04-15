using Common.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/v1/ping")]
    public class PingController : Controller
    {

        [HttpGet]
        [ActionName("get")]
        public bool Get()
        {
            return true;
        }
    }
}