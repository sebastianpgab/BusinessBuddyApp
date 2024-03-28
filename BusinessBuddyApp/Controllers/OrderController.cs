using BusinessBuddyApp.Entities;
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
        public async Task<ActionResult<Order>> Get([FromRoute] int clientId, [FromRoute] int id)
        {
            var order = await _orderService.Get(clientId, id);
            return Ok(order);
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll([FromRoute] int clientId)
        {
            var orders = await _orderService.GetAll(clientId);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromRoute] Order order) 
        {
            await _orderService.Create(order);
            return Ok();
        }
    }
}
