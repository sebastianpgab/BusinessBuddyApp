using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Models;
using BusinessBuddyApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessBuddyApp.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto userDto)
        {
            _accountService.RegisterUser(userDto);
            return Ok();
        }
    }
}
