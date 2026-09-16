using System.ComponentModel.DataAnnotations;

namespace MiniCalculatorMVC.Models
{
    public class CalculatorModel
    {
        [Required]
        public double FirstNumber { get; set; }
        [Required] 
        public double SecondNumber { get; set; }
        [Required]
        public string? Operation { get; set; }
        
        public double? Result { get; set; }

    }
}
