namespace backend.Modules.AuthModule.Dtos
{
    public class AuthRequestDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class AuthResponseDto
    {
        public required string Email { get; set; }
        public required string Token { get; set; }
        public required string WorkShopId { get; set; }
        public required string WorkShopName { get; set; }
    }
}
