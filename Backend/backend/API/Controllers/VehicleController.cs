using Microsoft.AspNetCore.Mvc;
using backend.Application.Services;
using backend.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _service;
        public VehicleController(IVehicleService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid workshopId) => Ok(await _service.GetAllAsync(workshopId));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleDto dto) => Ok(await _service.CreateAsync(dto));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateVehicleDto dto) { await _service.UpdateAsync(dto); return NoContent(); }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) { await _service.DeleteAsync(id); return NoContent(); }
    }
}
