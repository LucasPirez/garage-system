using Microsoft.AspNetCore.Mvc;

namespace backend.Modules.VehicleEntryModule
{
    [ApiController]
    [Route("[Controller]")]
    public class VehicleEntryController : ControllerBase
    {
        private readonly VehicleEntryService _vehicleEntryService;

        public VehicleEntryController(VehicleEntryService vehicleEntryService)
        {
            _vehicleEntryService = vehicleEntryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string workShopId)
        {
            var result = await _vehicleEntryService.GetAll();

            return Ok(result);
        }
    }
}
