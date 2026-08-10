
using BookStoreAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBooks ([FromRoute] int Id, [FromQuery] decimal maxPrice, [FromQuery] string author)
        {
            //return Ok($"Book Id = {Id}");
            return Ok($"Maximum Price = {maxPrice} : {author}");
        }
        [HttpPost]
        public IActionResult AddBooks([FromBody] CreateBookRequestDto request)
        {
            return Ok(request);
        }
        [HttpPost("upload")]
        public IActionResult UploadBook([FromForm] UploadBooksRequestDto request)
        {
            return Ok(new 
            {
                request.Title,
                request.Author,
                request.Price,
                FileName = request.Coverimage?.FileName
            });
        }
    }
}
