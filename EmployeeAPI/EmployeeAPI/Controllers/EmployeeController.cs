using EmployeeAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _service;
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmployeeController> _logger;
    public EmployeeController(EmployeeService service, IConfiguration configuration, ILogger<EmployeeController> logger)
    {
        Console.WriteLine("Controller Constructor");

        _service = service;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        Console.WriteLine("Controller Method");
        var name = _configuration["Compony:Address"];
        _logger.LogInformation("Employee Api Called");
        _logger.LogWarning("Employee Not Fount");
        //return Ok(_service.GetData());
        return Ok("Employee Data");
    }
}