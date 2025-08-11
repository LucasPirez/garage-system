using Application.Dtos.Vehicle;

namespace Application.Dtos.Customer
{
    public class CustomerWithVehiclesDto : BaseCustomerDto
    {
        public List<BaseVehicleDto> Vehicles { get; set; } = new List<BaseVehicleDto>();
    }
}
