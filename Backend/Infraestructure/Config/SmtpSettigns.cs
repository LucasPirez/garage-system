namespace Infraestructure.Config
{
    public class SmtpSettings
    {
        public const string Section = "EmailConfiguration";
        public string EmailEmitter { get; set; } = null!;
        public string Host { get; set; } = null!;
        public int Port { get; set; }
        public string Password { get; set; } = null!;
    }
}
