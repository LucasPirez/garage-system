using backend.Modules.VehicleEntryModule.Dtos;
using backend.Modules.VehicleEntryModule.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Modules.VehicleEntryModule
{
    [ApiController]
    [Route("vehicle-entry")]
    public class VehicleEntryController : ControllerBase
    {
        private readonly IVehicleEntryService _vehicleEntryService;

        public VehicleEntryController(IVehicleEntryService vehicleEntryService)
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
        public async Task<IActionResult> Create([FromBody] CreateVehicleEntryDto createDto)
        {
            var result = await _vehicleEntryService.CreateAsync(createDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
