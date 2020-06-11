using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBank.Cashback.Domain.Entities;
using MyBank.Cashback.Domain.Interface.Repositories;

namespace MyBank.Cashback.Api.Internal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientController> _logger;

        public ClientController(
            IClientRepository clientRepository,
            ILogger<ClientController> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return _clientRepository.ListAll();
        }

        [HttpPost]
        public ActionResult Post([FromBody] Client client)
        {
            try
            {
                return Ok(_clientRepository.Insert(client));
            }
            catch (Exception ex)
            {
                _logger.LogError("ClientController Post Error", ex);
                return BadRequest(ex.Message);
            }
        }

    }
}