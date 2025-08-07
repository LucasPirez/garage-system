using System.Net;
using System.Net.Mail;
using Application.Services;
using Infraestructure.Config;
using Microsoft.Extensions.Options;

namespace Infraestructure.Services
{
    public class SmtpService : ISmtpService
    {
        public readonly SmtpSettings _smtpSettings;

        public SmtpService(IOptions<SmtpSettings> smtpSettigns)
        {
            _smtpSettings = smtpSettigns.Value;
        }

        public async Task SendEmailAsync(string message, string recipient, string subject)
        {
            if (
                string.IsNullOrEmpty(_smtpSettings.Password)
                || string.IsNullOrEmpty(_smtpSettings.Host)
                || string.IsNullOrEmpty(_smtpSettings.EmailEmitter)
            )
            {
                throw new InvalidOperationException("Email configuration not set.");
            }
            var email = new MailMessage(_smtpSettings.EmailEmitter!, recipient, subject, message);

            await SetSmtpClient().SendMailAsync(email);
        }

        private SmtpClient SetSmtpClient()
        {
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
