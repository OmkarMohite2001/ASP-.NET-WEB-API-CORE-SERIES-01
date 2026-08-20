namespace EVChargingManagementAPI.DTOs
{
    public class CreateChargerDto
    {
        public string ChargerCode { get; set; } = string.Empty;
        public string ChargerType { get; set; } = string.Empty;
        public double PowerKW { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int ChargingStationId { get; set; }
    }

    public class UpdateChargerDto
    {
        public string ChargerCode { get; set; } = string.Empty;
        public string ChargerType { get; set; } = string.Empty;
        public double PowerKW { get; set; }
        public bool IsAvailable { get; set; }
        public int ChargingStationId { get; set; }
    }

    public class ChargerResponseDto
    {
        public int Id { get; set; }
        public string ChargerCode { get; set; } = string.Empty;
        public string ChargerType { get; set; } = string.Empty;
        public double PowerKW { get; set; }
        public bool IsAvailable { get; set; }
        public int ChargingStationId { get; set; }
    }

    public class ChargerWithSessionsDto
    {
        public int Id { get; set; }
        public string ChargerCode { get; set; } = string.Empty;
        public string ChargerType { get; set; } = string.Empty;
        public double PowerKW { get; set; }
        public bool IsAvailable { get; set; }
        public int ChargingStationId { get; set; }
        public List<ChargingSessionResponseDto> ChargingSessions { get; set; } = new();
    }
}
