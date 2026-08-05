namespace EmployeeAPI.Services;

public class EmployeeService
{
    public Guid ServiceId { get; } = Guid.NewGuid();

    public EmployeeService()
    {
        Console.WriteLine($"Service Created : {ServiceId}");
    }

    public string GetData()
    {
        return $"Employee Service : {ServiceId}";
    }
}