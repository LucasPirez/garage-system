﻿using System.Text.Json.Serialization;

namespace backend.Modules.VehicleEntryModule.Dtos
{
    public class CreateVehicleEntryDto
    {
        public DateTime ReceptionDate { get; set; } = DateTime.UtcNow;

        public string Cause { get; set; } = string.Empty;

        public string? Details { get; set; } = string.Empty;
        public double? Presupuest { get; set; }

        public required string VehicleId { get; set; }

        public required string WorkshopId { get; set; }

        public IList<string>? SpareParts { get; set; } = new List<string>();
    }
}
