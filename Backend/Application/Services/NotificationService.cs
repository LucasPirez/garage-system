using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public interface INotificationService
    {
        Task Notify(string message, string recipient, string subject);
    }

    public class SmtpSettings
    {
        public string EmailEmitter { get; set; } = null!;
        public string Host { get; set; } = null!;
        public int Port { get; set; }
        public string Password { get; set; } = null!;
    }

    public class NotificationService : INotificationService
    {
        private readonly SmtpSettings _smtpSettings;

        public NotificationService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task Notify(string message, string recipient, string subject)
        {
            var email = new MailMessage(_smtpSettings.EmailEmitter!, recipient, subject, message);

            await setSmtpClient().SendMailAsync(email);
        }

        private SmtpClient setSmtpClient()
        {
            if (
                string.IsNullOrEmpty(_smtpSettings.Password)
                || string.IsNullOrEmpty(_smtpSettings.Host)
                || string.IsNullOrEmpty(_smtpSettings.EmailEmitter)
            )
            {
                throw new InvalidOperationException("Email configuration not set.");
            }

            var smtpClient = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = new NetworkCredential(
                _smtpSettings.EmailEmitter,
                _smtpSettings.Password
            );

            return smtpClient;
        }
    }
}
