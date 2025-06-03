using backend.Database;
using backend.Database.Entites;
using backend.Modules.CustomerModule.Dtos;
using backend.Modules.VehicleEntryModule.Dtos;
using backend.Modules.VehicleModule.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace backend.Swagger
{
    public class CreateCustomerDtoExample : IExamplesProvider<CreateCustomerDto>
    {
        public CreateCustomerDto GetExamples() =>
            new()
            {
                WorkshopId = SeedData.workshopAId.ToString(),
                FirstName = "Juan",
                LastName = "Pérez",
                PhoneNumber = new List<string>() { "3422548239" },
                Email = new List<string>() { "3424372239" },
                Address = "Calle falsa 123",
                Dni = "12345678",
            };
    }

    public class CreateVehicleEntryDtoExample : IExamplesProvider<CreateVehicleEntryDto>
    {
        public CreateVehicleEntryDto GetExamples() =>
            new()
            {
                WorkshopId = SeedData.workshopAId.ToString(),
                Cause = "Cambio de aceite",
                Details = "Cambio de aceite y filtro",
                ReceptionDate = DateTime.UtcNow,
                Presupuest = 1000,
                SpareParts = new List<SpareParts>()
                {
                    new()
                    {
                        Name = "Aceite",
                        Price = 3000,
                        Quantity = 1,
                    },
                },
                VehicleId = "",
            };
    }

    public class CreateVehicleDtoExample : IExamplesProvider<CreateVehicleDto>
    {
        public CreateVehicleDto GetExamples() =>
            new()
            {
                CustomerId = SeedData.customerAId,
                Plate = "ab-394-35",
                Color = "Pink",
                Model = "Ranger Raptor",
            };
    }
}
