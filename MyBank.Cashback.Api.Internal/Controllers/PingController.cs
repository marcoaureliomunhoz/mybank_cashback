using Microsoft.AspNetCore.Mvc;

namespace MyBank.Cashback.Api.Internal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Pong";
        }
    }
}