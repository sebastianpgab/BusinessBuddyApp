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
        public ActionResult RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            _accountService.RegisterUser(registerUserDto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            string token = _accountService.GenerateJwt(loginUserDto);
            return Ok(token);
        }

        [HttpGet("checkApi")]
        public ActionResult CheckApi()
        {
            return Ok();
        }
    }
}
