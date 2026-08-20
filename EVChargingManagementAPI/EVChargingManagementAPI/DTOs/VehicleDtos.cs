namespace EVChargingManagementAPI.DTOs
{
    public class CreateVehicleDto
    {
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public double BatteryCapacityKWh { get; set; }
        public int CustomerId { get; set; }
    }

    public class UpdateVehicleDto
    {
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public double BatteryCapacityKWh { get; set; }
        public int CustomerId { get; set; }
    }

    public class VehicleResponseDto
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public double BatteryCapacityKWh { get; set; }
        public int CustomerId { get; set; }
    }

    public class VehicleWithSessionsDto
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public double BatteryCapacityKWh { get; set; }
        public int CustomerId { get; set; }
        public List<ChargingSessionResponseDto> ChargingSessions { get; set; } = new();
    }
}
