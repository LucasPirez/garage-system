namespace backend.Modules.RepairOrderModule.Dtos
{
    public class UpdateAmountAndStatusDto
    {
        public required double FinalAmount { get; set; }

        public required string Status { get; set; }

        public required string Id { get; set; }
    }

    public class UpdateSparePartDto
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
