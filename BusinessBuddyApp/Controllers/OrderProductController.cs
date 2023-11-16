using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessBuddyApp.Controllers
{
    [Route("api/orderDetailId/{orderDetailId}/orderProduct")]
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderProductService _orderProductService;
        public OrderProductController(IOrderProductService orderProductService)
        {
            _orderProductService = orderProductService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProduct>> Get([FromRoute]int id)
        {
            var orderProduct = await _orderProductService.Get(id);
            return Ok(orderProduct);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProduct>>> GetAll([FromRoute] int orderDetailId)
        {
            var orderProdcts = await _orderProductService.GetAll(orderDetailId);
            return Ok(orderProdcts);
        }

        [HttpPut("{id}")]
        public ActionResult<bool> Update([FromBody] OrderProduct orderProduct, [FromRoute] int id) 
        {
           var isUpdated =  _orderProductService.Update(orderProduct, id);
            return Ok(isUpdated);
        }

        //tu się zastanowić czy tego nie usunąc 
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete([FromRoute] int id)
        {
            var isDeleted = _orderProductService.Delete(id);
            return Ok(isDeleted);
        }

        [HttpPost]
        public ActionResult<bool> Create([FromBody] OrderProduct product, [FromRoute] int orderDetailId)
        {
            var isCreated = _orderProductService.Create(product, orderDetailId);
            return Ok(isCreated);
        }


    }
}
