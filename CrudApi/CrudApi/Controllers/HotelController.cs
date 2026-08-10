using Microsoft.AspNetCore.Mvc;
using CrudApi.Services;
using CrudApi.DTOs;
namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly EmployeeService _service;
        public HotelController(EmployeeService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public IActionResult GetHotelReq(int id)
        {
            var employee = _service.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            var response = new HotelResponseDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,

            };

            return Ok(response);
        }
    }
}
