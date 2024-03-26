using BusinessBuddyApp.Models;
using BusinessBuddyApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessBuddyApp.Controllers
{
    [Route("api/purchase")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePurchase([FromBody] PurchaseDto purchase)
        {
            await _purchaseService.CreatePurchase(purchase);
            return Ok();


        }
    }
}
