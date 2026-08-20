namespace EVChargingManagementAPI.DTOs
{
    public class CreateChargingSessionDto
    {
        public int VehicleId { get; set; }
        public int ChargerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double EnergyConsumedKWh { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class UpdateChargingSessionDto
    {
        public int VehicleId { get; set; }
        public int ChargerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double EnergyConsumedKWh { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class ChargingSessionResponseDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int ChargerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double EnergyConsumedKWh { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class ChargingSessionDetailsDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int ChargerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double EnergyConsumedKWh { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
        public VehicleResponseDto? Vehicle { get; set; }
        public ChargerResponseDto? Charger { get; set; }
    }
}
