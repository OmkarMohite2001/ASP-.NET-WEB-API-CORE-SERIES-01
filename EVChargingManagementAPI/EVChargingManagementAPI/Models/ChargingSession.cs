namespace EVChargingManagementAPI.Models
{
    public class ChargingSession
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public int ChargerId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double EnergyConsumedKWh { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; } = string.Empty;

        public Vehicle? Vehicle { get; set; }

        public Charger? Charger { get; set; }
    }
}
