using Microsoft.AspNetCore.Mvc;
using MyBank.Cashback.Domain.Interface.Repositories;

namespace MyBank.Cashback.Api.Internal.Controllers
{
  // Get cashback balance of a client

  [Route("api/[controller]")]
    [ApiController]
    public class CashbackBalanceController : ControllerBase
    {
        private readonly ICashbackRepository _cashbackRepository;

        public CashbackBalanceController(ICashbackRepository cashbackRepository)
        {
            _cashbackRepository = cashbackRepository;
        }

        [HttpGet("{id}")]
        public decimal Get(int id)
        {
            return _cashbackRepository.GetBalance(id);
        }
    }
}