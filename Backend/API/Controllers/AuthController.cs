using Application.Dtos.Auth;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
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
        public async Task<IActionResult> RequestResetPassword(
            [FromBody] RequestResetPasswordDto dto
        )
        {
            string token = await _authService.GenerateAndSaveResetPasswordToken(dto.Email);

            _ = _authService
                .SendTokenResetPassword(email: dto.Email, baseLink: dto.Link, token: token)
                .ContinueWith(
                    task =>
                    {
                        if (task.Exception != null)
                        {
                            Console.WriteLine(
                                task.Exception.ToString() + "Error al enviar notificación"
                            );
                        }
                    },
                    TaskContinuationOptions.OnlyOnFaulted
                );

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
