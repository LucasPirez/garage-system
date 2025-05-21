<<<<<<< Updated upstream
﻿using backend.Database;
=======
﻿using backend.Database.Entites;
>>>>>>> Stashed changes
using backend.Modules.CustomerModule.Dtos;
using backend.Modules.CustomerModule.Interfaces;
using backend.Swagger;
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

<<<<<<< Updated upstream
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] string workshopId)
        {
            Guid g = new Guid(workshopId);
            var result = await _customerService.GetAllAsync(Guid.Parse(workshopId));
=======
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _customerService.GetByIdAsync(Guid.Parse(id));
>>>>>>> Stashed changes

            return Ok(result);
        }

        [SwaggerRequestExample(typeof(CreateCustomerDto), typeof(CreateCustomerDtoExample))]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerDto customerDto)
        {
            var result = await _customerService.CreateAsync(customerDto);

            return Ok(result);
        }
    }
}
