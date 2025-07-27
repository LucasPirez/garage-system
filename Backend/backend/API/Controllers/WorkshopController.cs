using Microsoft.AspNetCore.Mvc;
using backend.Application.Services;
using backend.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkshopController : ControllerBase
    {
        private readonly IWorkshopService _service;
        public WorkshopController(IWorkshopService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWorkshopDto dto) => Ok(await _service.CreateAsync(dto));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateWorkshopDto dto) { await _service.UpdateAsync(dto); return NoContent(); }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) { await _service.DeleteAsync(id); return NoContent(); }
    }
}
