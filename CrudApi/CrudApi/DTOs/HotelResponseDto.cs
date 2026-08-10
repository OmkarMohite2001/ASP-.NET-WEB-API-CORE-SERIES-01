using System.ComponentModel.DataAnnotations;

namespace CrudApi.DTOs
{
    public class HotelResponseDto
    {
        [Required]
        public int Id{get;set;}
        [Required]
        public string? Name { get; set; }
        [Required]
        [StringLength(50)]
        public string? Department { get; set; }
       

    }
}
