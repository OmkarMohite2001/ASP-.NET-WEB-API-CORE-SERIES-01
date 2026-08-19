namespace EVChargingManagementAPI.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        public string RegistrationNumber { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public double BatteryCapacityKWh { get; set; }

        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public ICollection<ChargingSession>? ChargingSessions { get; set; } = new List<ChargingSession>();
    }
}
