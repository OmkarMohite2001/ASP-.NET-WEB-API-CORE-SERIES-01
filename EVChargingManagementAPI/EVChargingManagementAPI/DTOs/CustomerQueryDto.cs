namespace EVChargingManagementAPI.DTOs
{
    public class CustomerQueryDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; } = "asc";
        public string? Search {  get; set; }
        public string? City { get; set; }
        public bool? IsActive { get; set; }
    }
}
