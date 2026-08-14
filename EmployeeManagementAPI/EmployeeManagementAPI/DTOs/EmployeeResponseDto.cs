namespace EmployeeManagementAPI.DTOs
{
    public class EmployeeResponseDto
    {
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public Decimal Salary { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
