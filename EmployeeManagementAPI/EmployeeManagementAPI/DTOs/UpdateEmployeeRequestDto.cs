using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.DTOs
{
    public class UpdateEmployeeRequestDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Department { get; set; }

        [Range(1, 10000000)]
        public decimal Salary { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        public bool IsActive { get; set; }
    }
}
