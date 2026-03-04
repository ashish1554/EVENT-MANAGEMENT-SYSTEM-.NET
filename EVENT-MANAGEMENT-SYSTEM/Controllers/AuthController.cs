using EVENT_MANAGEMENT_SYSTEM.DTOs;
using EVENT_MANAGEMENT_SYSTEM.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVENT_MANAGEMENT_SYSTEM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService service)
        {
            _authService = service;
        }
        [HttpPost("register")]
        public async Task<IActionResult>  Register(RegisterDto request)
        {
            var result = await _authService.Register(request);
            if (result==null)
            {
                return BadRequest("User Already Exists");
            }
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto requset)
        {
            var result = await _authService.Login(requset);
            if(result==null)
            {
                return BadRequest("Invalid Credentials");
            }
            return Ok(result);
        }


    }
}
