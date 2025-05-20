using backend.Modules.VehicleModule.Dtos;
using backend.Modules.VehicleModule.Interfaces;
using backend.Swagger;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace backend.Modules.VehicleModule
{
    [ApiController]
    [Route("workshops/{workshopId}/vehicle")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] string workshopId)
        {
            var result = await _vehicleService.GetAllAsync(Guid.Parse(workshopId));

            return Ok(result);
        }

        [SwaggerRequestExample(typeof(CreateVehicleDto), typeof(CreateVehicleDtoExample))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleDto createDto)
        {
            var result = await _vehicleService.CreateAsync(createDto);

            return Ok(result);
        }
    }
}
