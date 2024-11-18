using HotelChainAPI.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelChainAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenHelper _jwtTokenHelper;

        public AuthController(JwtTokenHelper jwtTokenHelper)
        {
            _jwtTokenHelper = jwtTokenHelper;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            
            if (loginModel.Username == "Bozidar" && loginModel.Password == "Bozidar123")
            {
                var token = _jwtTokenHelper.GenerateToken(loginModel.Username);

                return Ok(new { Token = token });
            }
            return Unauthorized();
        }
    }

    public class LoginModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }             
    }
}
