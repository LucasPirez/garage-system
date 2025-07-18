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
        private readonly IRepairOrderService _repairOrderService;

        public RepairOrderController(IRepairOrderService repairOrderService)
        {
            _repairOrderService = repairOrderService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var result = await _repairOrderService.GetByIdAsync(Guid.Parse(id));
            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRepairOrderDto createDto)
        {
            var result = await _repairOrderService.CreateAsync(createDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPost("with-vehicle")]
        public async Task<IActionResult> CreateWithVehicle([FromBody] CreateRepairOrderWithVehicleDto createDto)
        {
            await _repairOrderService.AddRepairOrder(createDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("with-vehicle-and-customer")]
        public async Task<IActionResult> CreateWithVehicle([FromBody] CreateRepairOrderWithVehicleAndCustomerDto createDto)
        {
            await _repairOrderService.AddRepairOrder(createDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateRepairOrderDto updateDto)
        {
            await _repairOrderService.UpdateAsync(updateDto);
            return StatusCode(StatusCodes.Status200OK);
        }

        [SwaggerRequestExample(typeof(UpdateAmountAndStatusDto), typeof(PatchRepairOrderDto))]
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] UpdateAmountAndStatusDto updateDto)
        {
            await _repairOrderService.UpdateStatusAndFinalAmount(updateDto);
            return StatusCode(StatusCodes.Status200OK);
        }

        [SwaggerRequestExample(typeof(List<UpdateSparePartDto>), typeof(List<PatchSparePartDto>))]
        [HttpPatch("{id}/spare-parts")]
        public async Task<IActionResult> PatchSpareParts([FromRoute] Guid id, [FromBody] List<UpdateSparePartDto> dto)
        {
            await _repairOrderService.UpdateSpareParts(dto, id);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
