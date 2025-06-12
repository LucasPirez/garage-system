using backend.Modules.RepairOrderModule.Dtos;
using backend.Modules.RepairOrderModule.Interfaces;
using backend.Modules.VehicleModule.Dtos;
using backend.Swagger;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace backend.Modules.RepairOrderModule
{
    [ApiController]
    [Route("repair-order")]
    public class RepairOrderController : ControllerBase
    {
        private readonly IRepairOrderService _vehicleEntryService;

        public RepairOrderController(IRepairOrderService vehicleEntryService)
        {
            _vehicleEntryService = vehicleEntryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var result = await _vehicleEntryService.GetByIdAsync(Guid.Parse(id));
            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRepairOrderDto createDto)
        {
            var result = await _vehicleEntryService.CreateAsync(createDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateRepairOrderDto updateDto)
        {
            await _vehicleEntryService.UpdateAsync(updateDto);
            return StatusCode(StatusCodes.Status200OK);
        }

        [SwaggerRequestExample(typeof(UpdateAmountAndStatusDto), typeof(PatchRepairOrderDto))]
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] UpdateAmountAndStatusDto updateDto)
        {
            await _vehicleEntryService.UpdateStatusAndFinalAmount(updateDto);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
