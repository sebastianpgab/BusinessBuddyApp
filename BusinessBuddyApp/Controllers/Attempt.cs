using Microsoft.AspNetCore.Mvc;

namespace BusinessBuddyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Attempt : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "elo";
        }


    }
}
