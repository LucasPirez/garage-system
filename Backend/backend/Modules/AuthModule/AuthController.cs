using backend.Modules.AuthModule.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace backend.Modules.AuthModule
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequestDto request)
        {
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }

        [HttpPatch("request-reset-password")]
        public async Task<IActionResult> RequestResetPassword([FromBody] RequestResetPasswordDto dto)
        {
            await _authService.SendTokenResetPassword(dto.Email);

            return Accepted();
        }

        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            await _authService.ChangePassword(dto.ResetPasswordToken, dto.Password);

            return Ok();
        }
    }
}
