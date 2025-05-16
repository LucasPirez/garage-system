using Microsoft.AspNetCore.Mvc;

namespace backend.Modules.CustomerModule
{
    [ApiController]
    [Route("[Controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(string workShopId = "3")
        {
            var result = await _customerService.GetAll();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(string workShopId)
        {
            //var result = await _customerService.AddWithDto();

            return Ok(result);
        }
    }
}
