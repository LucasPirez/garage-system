using backend.Database;
using backend.Database.Entites;
using backend.Modules.CustomerModule.Dtos;
using backend.Modules.CustomerModule.Interfaces;
using backend.Swagger;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace backend.Modules.CustomerModule
{
    [ApiController]
    [Route("customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _customerService.GetByIdAsync(Guid.Parse(id));

            return Ok(result);
        }

        [SwaggerRequestExample(typeof(CreateCustomerDto), typeof(CreateCustomerDtoExample))]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerDto customerDto)
        {
            var result = await _customerService.CreateAsync(customerDto);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateCustomerDto customerDto
        )
        {
            await _customerService.UpdateAsync(id, customerDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _customerService.DeleteAsync(Guid.Parse(id));

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
