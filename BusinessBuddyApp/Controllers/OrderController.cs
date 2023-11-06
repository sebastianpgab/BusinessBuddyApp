﻿using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessBuddyApp.Controllers
{
    [Route("api/client/{clientId}/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get([FromRoute] int id)
        {
            var order = await _orderService.Get(id);
            return Ok(order);
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            var orders = await _orderService.GetAll();
            return Ok(orders);
        }


        [HttpPost]
        public ActionResult<bool> Post([FromRoute] int clientId) 
        {
            var isCreated = _orderService.Create(clientId);
            return Ok(isCreated);
        }

    }
}