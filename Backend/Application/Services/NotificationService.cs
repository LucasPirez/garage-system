using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public interface ISmtpService
    {
        Task SendEmailAsync(string message, string recipient, string subject);
    }

    public interface INotificationService
    {
        Task Notify(string message, string recipient, string subject);
    }

    public class NotificationService : INotificationService
    {
        private readonly ISmtpService _smtpService;

        public NotificationService(ISmtpService smtpService)
        {
            _smtpService = smtpService;
        }

        public async Task Notify(string message, string recipient, string subject)
        {
            await _smtpService.SendEmailAsync(
                message: message,
                recipient: recipient,
                subject: subject
            );
        }
    }
}
