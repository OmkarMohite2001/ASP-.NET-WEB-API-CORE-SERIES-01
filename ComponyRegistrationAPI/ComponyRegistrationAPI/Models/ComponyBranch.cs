using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ComponyRegistrationAPI.Models
{
    public class ComponyBranch
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string BranchName { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [JsonIgnore]
        public Compony? Compony { get; set; } 
    }
}
