using CrudApi.Model;
using CrudApi.Services;
using CrudApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers
{
    [Route("api/omkar")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var employees = _service.GetEmployees();
            return Ok(employees);
        }
        //public IActionResult AddEmployee(Employee employee)
        //{
        //     _service.AddEmployee(employee);
        //    return Ok();
        //}
        [HttpPost]
        public IActionResult AddEmployee(CreateEmployeeRequestDto request)
        {
            
            return Ok(request);
        }
        [HttpGet("{id}")]
        public IActionResult getById(int id)
        {
            var employee = _service.GetEmployeeById(id);
            if(employee == null)
            {
                return NotFound("Employee Not Found");
            }
            return Ok(employee);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id,Employee employee)
        {
           bool result = _service.UpdateEmployee(id, employee);
            if(!result)
            {
                return NotFound("Employee not Found");
            }
            return Ok("Employee Updated Successfully");
        }
        [HttpDelete("{id}/{omkar}")]
        public IActionResult DeleteEmployee(int id)
        {
            bool result = (_service.DeleteEmployee(id));
            if(!result)
            {
                return NotFound("Employee Not Found");
            }
            return Ok("Employee Deleted Successfully");
        }
        [HttpPatch("{id}/salary")]
        public IActionResult UpdateSalary(int id, decimal salary)
        {
          bool result = _service.UpdateSalary(id, salary);
            if(!result)
            {
                return NotFound("Employee Not Found");
            }
            return Ok("Salary Updated");
        }
    }
}
