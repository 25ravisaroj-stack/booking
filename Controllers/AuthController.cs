using BookingSystemApi.Models;
using BookingSystemApi.Services;
using Microsoft.AspNetCore.Mvc;
using BookingSystemApi.Logins;

namespace BookingSystemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response =await _userService.LoginAsync(request);

            if (response == null)
            {
                return Unauthorized("Invalid Credentials");
            }

            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var response =await _userService.RefreshTokenAsync(request);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }
    }
}