namespace EVChargingManagementAPI.Models
{
    public class Charger
    {
        public int Id { get; set; }

        public string ChargerCode { get; set; } = string.Empty;

        public string ChargerType { get; set; } = string.Empty;

        public double PowerKW { get; set; }

        public bool IsAvailable { get; set; }

        public int ChargingStationId { get; set; }

        public ChargingStation? ChargingStation { get; set; }

        public ICollection<ChargingSession>? ChargingSessions { get; set; } = new List<ChargingSession>();
    }
}
