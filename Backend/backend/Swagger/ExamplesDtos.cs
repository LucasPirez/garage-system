﻿using backend.Database;
using backend.Database.Entites;
using backend.Modules.CustomerModule.Dtos;
using backend.Modules.RepairOrderModule.Dtos;
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

    public class CreateVehicleEntryDtoExample : IExamplesProvider<CreateRepairOrderDto>
    {
        public CreateRepairOrderDto GetExamples() =>
            new()
            {
                Cause = "Cambio de aceite",
                Details = "Cambio de aceite y filtro",
                ReceptionDate = DateTime.UtcNow,
                VehicleId = "",
            };
    }

    public class UpdateJboDtoExample : IExamplesProvider<UpdateRepairOrderDto>
    {
        public UpdateRepairOrderDto GetExamples() =>
            new()
            {
                Cause = "Cambio de aceite",
                Details = "Cambio de aceite y filtro",
                ReceptionDate = DateTime.UtcNow,
                Budget = 1000,
                Id = "",
                SpareParts = new List<SparePart>()
                {
                    new()
                    {
                        Name = "Aceite",
                        Price = 3000,
                        Quantity = 1,
                    },
                },
                DeliveryDate = DateTime.UtcNow,
                FinalAmount = 1000,
                NotificationSent = false,
            };
    }

    public class PatchRepairOrderDto : IExamplesProvider<UpdateAmountAndStatusDto>
    {
        public UpdateAmountAndStatusDto GetExamples() =>
            new()
            {
                Id = "",
                FinalAmount = 1000,
                Status = RepairOrderStatus.InProgress.ToString(),
            };
    }

    public class PatchSparePartDto : IExamplesProvider<UpdateSparePartDto>
    {
        UpdateSparePartDto IExamplesProvider<UpdateSparePartDto>.GetExamples() =>
             new UpdateSparePartDto
                    {
                        Name = "Aceite",
                        Price = 4500,
                        Quantity = 1
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
