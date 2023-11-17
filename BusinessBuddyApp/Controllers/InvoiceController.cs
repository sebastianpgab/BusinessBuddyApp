using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessBuddyApp.Controllers
{
    [Route("api/invoice")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> Get([FromRoute]int id)
        {
            var invoice = await _invoiceService.Get(id);
            return Ok(invoice);
        }

        [HttpPost("order/{id}")]
        public ActionResult<bool> Create([FromBody]Invoice invoice, [FromRoute] int orderId)
        {
            var isCreated = _invoiceService.Create(invoice, orderId);
            return Ok(isCreated);
        }
    }
}
