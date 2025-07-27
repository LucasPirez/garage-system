using System;

namespace backend.Application.Dtos
{
    public class CreateNotificationDto
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public Guid? UserId { get; set; }
    }
    public class UpdateNotificationDto : CreateNotificationDto
    {
        public Guid Id { get; set; }
    }
}
