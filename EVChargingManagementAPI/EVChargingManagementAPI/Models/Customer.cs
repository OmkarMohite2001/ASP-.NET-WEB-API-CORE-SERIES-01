namespace EVChargingManagementAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public ICollection<Vehicle>? Vehicles { get; set; } = new List<Vehicle>();
    }
}
