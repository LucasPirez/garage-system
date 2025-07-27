using System.Net;
using System.Net.Mail;
using Application.Dtos;
using Domain;

namespace Application.Services
{
    public interface NotificationService
    {
        Task Notify(string message, string recipient, string subject);
    }

    public class NotificationService : NotificationService
    {
        private readonly IConfiguration _configuration;

        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Notify(string message, string recipient, string subject)
        {
            string? emailEmiter = _configuration.GetValue<string>("EmailConfiguration:Email");

            var email = new MailMessage(emailEmiter!, recipient, subject, message);

            await setSmtpClient().SendMailAsync(email);
        }

        private SmtpClient setSmtpClient()
        {
            string? password = _configuration.GetValue<string>("EmailConfiguration:Password");
            string? host = _configuration.GetValue<string>("EmailConfiguration:Host");
            int port = _configuration.GetValue<int>("EmailConfiguration:Port");
            string? emailEmiter = _configuration.GetValue<string>("EmailConfiguration:Email");

            if (
                string.IsNullOrEmpty(password)
                || string.IsNullOrEmpty(host)
                || string.IsNullOrEmpty(emailEmiter)
            )
            {
                throw new InvalidOperationException("Email configuration not set.");
            }

            var smtpClient = new SmtpClient(host, port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = new NetworkCredential(emailEmiter, password);

            return smtpClient;
        }
    }
}
