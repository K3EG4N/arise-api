using arise_api.dtos.Generics;
using arise_api.dtos.Request;
using arise_api.dtos.Responses;
using arise_api.services;
using Microsoft.AspNetCore.Mvc;

namespace arise_api.controllers
{
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : Controller
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<BaseResponse>> Register([FromBody] RegisterRequest request)
        {
            var response = await _authService.RegisterAsync(request);
            return Ok(response);
        }
    }
}
