﻿using backend.Database.Entites;

namespace backend.Modules.VehicleModule.Dtos
{
    public class HistoricalRepairOrderDto
    {
        public string Id { get; set; }
        public DateTime ReceptionDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeliveryDate { get; set; }

        public bool NotifycationSent { get; set; } = false;

        public string Status { get; set; }

        public string Cause { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;
        public double Budget { get; set; }
        public double FinalAmount { get; set; }

        public IList<SparePart> SpareParts { get; set; } = new List<SparePart>();
    }
}
