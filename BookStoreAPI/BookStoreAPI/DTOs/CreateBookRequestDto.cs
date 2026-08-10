namespace BookStoreAPI.DTOs
{
    public class CreateBookRequestDto
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public decimal Price { get; set; }
    }
}
