using Application.Dtos.Vehicle;

namespace Application.Dtos.Customer
{
    public class CustomerWithVehiclesDto : BaseCustomerDto
    {
        public List<VehicleDto> Vehicles { get; set; } = new List<VehicleDto>();
    }
}
