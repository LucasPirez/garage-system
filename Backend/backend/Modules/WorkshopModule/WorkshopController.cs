using backend.Modules.CustomerModule.Interfaces;
using backend.Modules.VehicleEntryModule.Interfaces;
using backend.Modules.VehicleModule.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Modules.WorkshopModule
{
    [ApiController]
    [Route("workshops")]
    public class WorkshopController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IVehicleService _vehicleService;
        private readonly IVehicleEntryService _vehicleEntryService;

        public WorkshopController(
            ICustomerService customerService,
            IVehicleService vehicleService,
            IVehicleEntryService vehicleEntryService
        )
        {
            _customerService = customerService;
            _vehicleService = vehicleService;
            _vehicleEntryService = vehicleEntryService;
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

        [HttpGet("{workshopId}/vehicles-entries")]
        public async Task<IActionResult> GetAllVehicleEntries([FromRoute] string workshopId)
        {
            var result = await _vehicleEntryService.GetAllAsync(Guid.Parse(workshopId));

            return Ok(result);
        }
    }
}
