using backend.Database;
using backend.Modules.CustomerModule.Dtos;
using backend.Modules.CustomerModule.Interfaces;
using backend.Swagger;
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

        [SwaggerRequestExample(typeof(CreateCustomerDto), typeof(CreateCustomerDtoExample))]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateCustomerDto customerDto,
            [FromRoute] string workshopId
        )
        {
            customerDto.SetWorkshopId(Guid.Parse(workshopId));
            var result = await _customerService.CreateAsync(customerDto);

            return Ok(result);
        }
    }
}
