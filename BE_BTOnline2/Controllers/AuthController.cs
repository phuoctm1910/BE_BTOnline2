using BE_BTOnline2.Models.Requests;
using BE_BTOnline2.Models.Responses;
using BE_BTOnline2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BE_BTOnline2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authService;

        public AuthController(IAuthServices authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthRequest request)
        {
            var response = _authService.Authenticate(request);
            if (response == null)
            {
                return Unauthorized(new { message = "Email hay mật khẩu không chính xác" });
            }
            return Ok(response);
        }
    }
}
