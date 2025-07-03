using backend.Modules.VehicleModule.Dtos;
using backend.Modules.VehicleModule.Interfaces;
using backend.Swagger;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace backend.Modules.VehicleModule
{
    [ApiController]
    [Route("vehicle")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [SwaggerRequestExample(typeof(CreateVehicleDto), typeof(CreateVehicleDtoExample))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleDto createDto)
        {
            var result = await _vehicleService.CreateAsync(createDto);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateVehicleDto vehicleDto
        )
        {
            await _vehicleService.UpdateAsync(id, vehicleDto);
            return Ok();
        }

        [HttpGet("{id}/repair-orders")]
        public async Task<IActionResult> GetRepairOrdersByVehicleId(
            [FromRoute] Guid id,
            int limit = 10
        )
        {
            var result = await _vehicleService.GetRepairOrderByVehicleIdAsync(id, limit);

            return new OkObjectResult(result);
        }
    }
}
