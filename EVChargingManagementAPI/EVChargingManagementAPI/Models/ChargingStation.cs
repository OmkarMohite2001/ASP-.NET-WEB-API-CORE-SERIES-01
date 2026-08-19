using System.Runtime.InteropServices;

namespace EVChargingManagementAPI.Models
{
    public class ChargingStation
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public ICollection<Charger>? Chargers { get; set; } = new List<Charger>();
    }
}
