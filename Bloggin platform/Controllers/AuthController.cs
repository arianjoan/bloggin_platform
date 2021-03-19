using Bloggin_platform.Dtos.User;
using Bloggin_platform.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggin_platform.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] UserAuthDto userCredentials)
        {
            var token = _authService.Authenticate(userCredentials.UserName, userCredentials.Password);

            if (token == null)
                return Unauthorized("The user credentials are wrong");

            return Ok(token);
        }
    }
}
