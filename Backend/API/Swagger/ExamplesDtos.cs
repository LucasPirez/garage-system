using Application.Dtos.Customer;
using Application.Dtos.RepairOrder;
using Application.Dtos.Vehicle;
using Infraestructure.Context;
using Infraestructure.DataModel;
using Swashbuckle.AspNetCore.Filters;

namespace API.Swagger
{
    public class CreateCustomerDtoExample : IExamplesProvider<CreateCustomerDto>
    {
        public CreateCustomerDto GetExamples() =>
            new()
            {
                Id = Guid.NewGuid(),
                WorkshopId = SeedData.workshopAId.ToString(),
                FirstName = "Juan",
                LastName = "Pérez",
                PhoneNumber = new List<string>() { "3422548239" },
                Email = new List<string>() { "3424372239" },
                Address = "Calle falsa 123",
                Dni = "12345678",
            };
    }

    public class CreateVehicleEntryDtoExample : IExamplesProvider<CreateRepairOrderDto>
    {
        public CreateRepairOrderDto GetExamples() =>
            new()
            {
                Id = Guid.NewGuid(),
                Cause = "Cambio de aceite",
                Details = "Cambio de aceite y filtro",
                ReceptionDate = DateTime.UtcNow,
                WorkshopId = SeedData.workshopAId.ToString(),
                Vehicle = new VehicleDto()
                {
                    CustomerId = Guid.NewGuid().ToString(),
                    Id = Guid.NewGuid(),
                    Plate = "ab-394-35",
                    Color = "Pink",
                    Model = "Ranger Raptor",
                },
            };
    }

    //public class UpdateJboDtoExample : IExamplesProvider<UpdateRepairOrderDto>
    //{
    //    public UpdateRepairOrderDto GetExamples() =>
    //        new()
    //        {
    //            Cause = "Cambio de aceite",
    //            Details = "Cambio de aceite y filtro",
    //            ReceptionDate = DateTime.UtcNow,
    //            Budget = 1000,
    //            Id = "",
    //            SpareParts = new List<SparePart>()
    //            {
    //                new()
    //                {
    //                    Name = "Aceite",
    //                    Price = 3000,
    //                    Quantity = 1,
    //                },
    //            },
    //            DeliveryDate = DateTime.UtcNow,
    //            FinalAmount = 1000,
    //            NotificationSent = false,
    //        };
    //}

    public class PatchRepairOrderExample : IExamplesProvider<UpdateAmountAndStatusDto>
    {
        public UpdateAmountAndStatusDto GetExamples() =>
            new()
            {
                Id = Guid.NewGuid(),
                FinalAmount = 1000,
                Status = EFRepairOrderStatus.InProgress.ToString(),
            };
    }

    public class PatchSparePartExample : IExamplesProvider<UpdateSparePartDto>
    {
        UpdateSparePartDto IExamplesProvider<UpdateSparePartDto>.GetExamples() =>
            new UpdateSparePartDto
            {
                Name = "Aceite",
                Price = 4500,
                Quantity = 1,
            };
    }

    public class CreateVehicleDtoExample : IExamplesProvider<CreateVehicleDto>
    {
        public CreateVehicleDto GetExamples() =>
            new()
            {
                Id = Guid.NewGuid(),
                CustomerId = SeedData.customerAId,
                Plate = "ab-394-35",
                Color = "Pink",
                Model = "Ranger Raptor",
            };
    }
}
