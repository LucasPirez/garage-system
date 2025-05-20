using backend.Modules.VehicleEntryModule.Dtos;
using backend.Modules.VehicleEntryModule.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Modules.VehicleEntryModule
{
    [ApiController]
    [Route("workshops/{workshopId}/vehicle-entry")]
    public class VehicleEntryController : ControllerBase
    {
        private readonly IVehicleEntryService _vehicleEntryService;

        public VehicleEntryController(IVehicleEntryService vehicleEntryService)
        {
            _vehicleEntryService = vehicleEntryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] string workshopId)
        {
            var result = await _vehicleEntryService.GetAllAsync(Guid.Parse(workshopId));

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] string workshopId,
            [FromRoute] string id
        )
        {
            var result = await _vehicleEntryService.GetByIdAsync(Guid.Parse(id));
            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromRoute] string workshopId,
            [FromBody] CreateVehicleEntryDto createDto
        )
        {
            createDto.SetWorkshopId(Guid.Parse(workshopId));
            var result = await _vehicleEntryService.CreateAsync(createDto);
            return new OkObjectResult(result);
        }
    }
}
