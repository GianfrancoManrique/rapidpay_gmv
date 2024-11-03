using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RapidPay.General.MockData;
using RapidPay.General.Services;

namespace RapidPay.General.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IJwtTokenService _jwtTokenService;

        public AuthController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Simulate a user validation. Replace this with your own user validation logic.
            var existingUser = MockAuthData.Users.FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);
            if (existingUser != null && !string.IsNullOrEmpty(existingUser.UserId))
            {
                var token = _jwtTokenService.GenerateToken(existingUser.UserId);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
    }
}
