using EmployeeAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _service;

    public EmployeeController(EmployeeService service)
    {
        Console.WriteLine("Controller Constructor");

        _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        Console.WriteLine("Controller Method");

        return Ok(_service.GetData());
    }
}