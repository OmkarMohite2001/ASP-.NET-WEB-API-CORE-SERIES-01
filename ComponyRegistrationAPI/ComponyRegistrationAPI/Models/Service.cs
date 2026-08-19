using System.Text.Json.Serialization;

namespace ComponyRegistrationAPI.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string? ServiceName { get; set; } = string.Empty;
        public string? Description {  get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<Compony>? Componies { get; set; } = new List<Compony>();
    }
}
