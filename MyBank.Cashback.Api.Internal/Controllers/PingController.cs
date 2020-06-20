using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyBank.Cashback.Api.Internal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly ILogger<PingController> _logger;

        public PingController(ILogger<PingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("PingController Get");
            return "Pong";
        }
    }
}