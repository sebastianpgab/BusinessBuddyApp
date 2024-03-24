using BusinessBuddyApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace BusinessBuddyApp.Controllers
{
    [Route("api/purchase")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> CreatePurchase([FromBody] PurchaseDto purchase)
        {
            
            
        }
    }
}
