using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyBank.Cashback.Domain.Entities;
using MyBank.Cashback.Domain.Interface.Repositories;

namespace MyBank.Cashback.Api.Internal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashbackStatementController : ControllerBase
    {
      private readonly ICashbackRepository _cashbackRepository;

      public CashbackStatementController(ICashbackRepository cashbackRepository)
      {
          _cashbackRepository = cashbackRepository;
      }

      [HttpGet("{id}")]
      public IEnumerable<CashbackAccount> Get(int id)
      {
        return _cashbackRepository.ListAll(id);
      }
    }
}