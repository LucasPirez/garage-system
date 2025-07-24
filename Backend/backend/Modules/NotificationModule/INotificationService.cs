namespace backend.Modules.NotificationModule
{
    public interface INotificationService
    {
        Task Notify(string message, string recipient = default);

    }
}
