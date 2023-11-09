using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessBuddyApp.Controllers
{
    [Route("/api/order/{orderId}/orderDetail")]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> Get([FromRoute] int id)
        {
            var orderDetail = await _orderDetailService.Get(id);
            return Ok(orderDetail);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetAll()
        {
            var orderDetails = await _orderDetailService.GetAll();
            return Ok(orderDetails);
        }

        [HttpPost]
        public ActionResult<bool> Create([FromRoute] int orderId)
        {
            var isCreated = _orderDetailService.Create(orderId);
            return Ok(isCreated);
        }
    }
}
