using API.Swagger;
using Application.Dtos.Customer;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace API.Controllers
{
    [ApiController]
    [Route("customer")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
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
            await _customerService.CreateAsync(customerDto);

            return Created();
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
