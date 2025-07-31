using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("workshops")]
    public class WorkshopController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public WorkshopController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{workshopId}/customers")]
        public async Task<IActionResult> GetAllCustomers([FromRoute] string workshopId)
        {
            var result = await _customerService.GetAllAsync(Guid.Parse(workshopId));

            return Ok(result);
        }

        //[HttpGet("{workshopId}/vehicles")]
        //public async Task<IActionResult> GetAllVehicles([FromRoute] string workshopId)
        //{
        //    var result = await _vehicleService.GetAllAsync(Guid.Parse(workshopId));

        //    return Ok(result);
        //}

        //[HttpGet("{workshopId}/repair-order")]
        //public async Task<IActionResult> GetAllVehicleEntries([FromRoute] string workshopId)
        //{
        //    var result = await _vehicleEntryService.GetAllAsync(Guid.Parse(workshopId));

        //    return Ok(result);
        //}
    }
}
