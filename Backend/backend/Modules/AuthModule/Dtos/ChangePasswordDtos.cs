namespace backend.Modules.AuthModule.Dtos
{

    public class RequestResetPasswordDto
    {
        public required string Email { get; set; }
    }

    public class ChangePasswordDto
    {
        public required string Password { get; set; }

        public required string ResetPasswordToken { get; set; }
    }
}