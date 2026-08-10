namespace BookStoreAPI.DTOs
{
    public class UploadBooksRequestDto
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public decimal Price { get; set; }
        public IFormFile? Coverimage {  get; set; }
    }
}
