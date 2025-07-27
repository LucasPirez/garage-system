using System;

namespace backend.Application.Dtos
{
    public class CreateWorkshopDto
    {
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
    public class UpdateWorkshopDto : CreateWorkshopDto
    {
        public Guid Id { get; set; }
    }
}
