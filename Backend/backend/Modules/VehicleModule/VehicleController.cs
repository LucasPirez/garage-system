using Microsoft.AspNetCore.Mvc;

namespace backend.Modules.VehicleModule
{
    [ApiController]
    [Route("[Controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly VehicleService _vehicleService;

        public VehicleController(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string workShopId)
        {
            var result = await _vehicleService.GetAll();

            return Ok(result);
        }
    }
}
