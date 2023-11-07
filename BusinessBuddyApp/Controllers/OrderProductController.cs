using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessBuddyApp.Controllers
{
    [Route("api/orderDetail/{orderId}/orderProduct")]
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderProductService _orderProductService;
        public OrderProductController(IOrderProductService orderProductService)
        {
            _orderProductService = orderProductService;
        }

        [HttpGet("{id}")]
        public ActionResult<OrderProduct> Get([FromRoute]int id)
        {
            var orderProduct = _orderProductService.Get(id);
            return Ok(orderProduct);
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderProduct>> GetAll([FromRoute] int orderId)
        {
            var orderProdcts = _orderProductService.GetAll(orderId);
            return Ok(orderProdcts);
        }

        [HttpPut("{id}")]
        public ActionResult<bool> Update([FromBody] OrderProduct orderProduct, [FromRoute] int id) 
        {
           var isUpdated =  _orderProductService.Update(orderProduct, id);
            return Ok(isUpdated);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete([FromRoute] int id)
        {
            var isDeleted = _orderProductService.Delete(id);
            return Ok(isDeleted);
        }

        [HttpPost]
        public ActionResult<OrderProduct> Create([FromBody] OrderProduct product)
        {
            var orderProduct = _orderProductService.Create(product);
            return(orderProduct);
        }


    }
}
