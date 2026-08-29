namespace EVChargingManagementAPI.Models
{
    public class VehicleDocument
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public DateTime UploadedAt { get; set; }

        public Vehicle? Vehicle { get; set; }
    }
}
