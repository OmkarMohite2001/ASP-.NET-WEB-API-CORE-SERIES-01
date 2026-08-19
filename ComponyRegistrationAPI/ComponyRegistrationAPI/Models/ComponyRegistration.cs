using System.Text.Json.Serialization;

namespace ComponyRegistrationAPI.Models
{
    public class ComponyRegistration
    {
        public int Id { get; set; }             //Primary key
        public int ComponyId { get; set; }      //Foreign key
        public string? RegistrationNumber { get; set; } = string.Empty;
        public DateTime RegistrationDate {  get; set; }
        public string? RegistrationAuthority { get; set; } = string.Empty;
        [JsonIgnore]
        public Compony? Compony { get; set; } = null;
    }
}
