using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("workshops")]
    public class WorkshopController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IVehicleService _vehicleService;
        private readonly IRepairOrderService _repairOrderService;

        public WorkshopController(
            ICustomerService customerService,
            IVehicleService vehicleService,
            IRepairOrderService repairOrderService
        )
        {
            _customerService = customerService;
            _vehicleService = vehicleService;
            _repairOrderService = repairOrderService;
        }

        [HttpGet("{workshopId}/customers")]
        public async Task<IActionResult> GetAllCustomers([FromRoute] string workshopId)
        {
            var result = await _customerService.GetAllAsync(Guid.Parse(workshopId));

            return Ok(result);
        }

        [HttpGet("{workshopId}/vehicles")]
        public async Task<IActionResult> GetAllVehicles([FromRoute] string workshopId)
        {
            var result = await _vehicleService.GetAllAsync(Guid.Parse(workshopId));

            return Ok(result);
        }

        [HttpGet("{workshopId}/repair-order")]
        public async Task<IActionResult> GetAllVehicleEntries([FromRoute] Guid workshopId)
        {
            var result = await _repairOrderService.GetAllAsync(workshopId);

            return Ok(result);
        }
    }
}
