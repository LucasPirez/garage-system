namespace backend.Modules.RepairOrderModule.Dtos
{
    public class UpdateAmountAndStatusDto
    {
        public required double FinalAmount { get; set; }

        public required string Status { get; set; }

        public required string Id { get; set; }
    }
}
