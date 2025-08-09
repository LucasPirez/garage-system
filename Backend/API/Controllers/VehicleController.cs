using API.Swagger;
using Application.Dtos.Vehicle;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace API.Controllers
{
    [ApiController]
    [Route("vehicle")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IRepairOrderService _repairOrderService;

        public VehicleController(
            IVehicleService vehicleService,
            IRepairOrderService repairOrderService
        )
        {
            _vehicleService = vehicleService;
            _repairOrderService = repairOrderService;
        }

        [SwaggerRequestExample(typeof(CreateVehicleDto), typeof(CreateVehicleDtoExample))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleDto createDto)
        {
            await _vehicleService.CreateAsync(createDto);

            return StatusCode(StatusCodes.Status201Created);
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
            var result = await _repairOrderService.GetByVehicleIdAsync(id, limit);

            return new OkObjectResult(result);
        }
    }
}
