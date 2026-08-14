using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        public readonly IEmployeeRepository? _repository;
        public EmployeeController(IEmployeeRepository? repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _repository.GetAll();
            return Ok(employees);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var employee = _repository.GetById(id);
            if(employee == null)
            {
                return NotFound("Employee Not Found");

            }
            return Ok(employee);
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeRequestDto request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var emplyee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Department = request.Department,
                Salary = request.Salary,
                JoiningDate = request.JoiningDate,
            };
            var employeeId = _repository.Create(emplyee);

            return Ok(new
            {

                EmployeeId = employeeId,
                Message = "Employee Created Successfuly"
            }
                    
                );  
        }
        [HttpPut("{id}")]
        public IActionResult Update(
        int id,
        UpdateEmployeeRequestDto request)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var employee = new Employee
                {
                    EmployeeId = id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Department = request.Department,
                    Salary = request.Salary,
                    JoiningDate = request.JoiningDate,
                    IsActive = request.IsActive
                };

                var updated = _repository.Update(employee);

                if (!updated)
                {
                    return NotFound("Employee not found");
                }

                return Ok("Employee updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _repository.Delete(id);

            if (!deleted)
            {
                return NotFound("Employee not found");
            }

            return NoContent();
        }

    }
}
