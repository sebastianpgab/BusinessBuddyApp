﻿using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Models;
using BusinessBuddyApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessBuddyApp.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        public IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public ActionResult<ICollection<Client>> GetAll([FromQuery] ClientQuery clientQuery)
        {
            var clients = _clientService.GetAll(clientQuery);
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public ActionResult<Client> Get([FromRoute] int id)
        {
            var client = _clientService.Get(id);
            return Ok(client);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Client>> Update([FromBody] Client client, [FromRoute] int id)
        {
            var updatedClient = await _clientService.Update(client, id);
            return Ok(updatedClient);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ClientDto clientDto)
        {
            await _clientService.Create(clientDto);
            return Ok();
        }

    }
}
