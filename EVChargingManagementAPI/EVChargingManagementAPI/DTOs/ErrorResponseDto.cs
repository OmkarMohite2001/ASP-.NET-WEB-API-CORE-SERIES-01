namespace EVChargingManagementAPI.DTOs
{
    public class ErrorResponseDto
    {
        public int StatusCode {  get; set; }
        public string? Message { get; set; }=string.Empty;
        public string? Detail { get; set; }
    }
}
//{"statusCode": 500,"Message": "Something went wrong","details":null}