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
    [Route("workshops/{workshopId}/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] string workshopId)
        {
            Guid g = new Guid(workshopId);
            var result = await _customerService.GetAllAsync(Guid.Parse(workshopId));

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _customerService.GetByIdAsync(Guid.Parse(id));

            return Ok(result);
        }

        [SwaggerRequestExample(typeof(CreateCustomerDto), typeof(CreateCustomerDtoExample))]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateCustomerDto customerDto,
            [FromRoute] string workshopId
        )
        {
            customerDto.SetWorkshopId(Guid.Parse(workshopId));
            var result = await _customerService.CreateAsync(customerDto);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] string id,
            [FromBody] UpdateCustomerDto customerDto
        )
        {
            var Customer = await _customerService.GetByIdAsync(Guid.Parse(id));

            if (Customer == null)
            {
                return NotFound();
            }
            // lo tengo que poner en el servicio.
            Customer.PhoneNumber = customerDto?.PhoneNumber ?? Customer.PhoneNumber;
            Customer.Email = customerDto?.Email ?? Customer.Email;
            Customer.Address = customerDto?.Address ?? Customer.Address;
            Customer.Dni = customerDto?.Dni ?? Customer.Dni;
            Customer.FirstName = customerDto?.FirstName ?? Customer.FirstName;
            Customer.LastName = customerDto?.LastName ?? Customer.LastName;

            await _customerService.UpdateAsync(Customer);

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _customerService.DeleteAsync(Guid.Parse(id));

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
