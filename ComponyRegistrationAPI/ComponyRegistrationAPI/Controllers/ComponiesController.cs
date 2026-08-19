using ComponyRegistrationAPI.Data;
using ComponyRegistrationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComponyRegistrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponiesController : ControllerBase
    {
        private readonly AppDbContext? _context;
        public ComponiesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCompony(Compony compony)
        {
            _context.Componies.Add(compony);
            await _context.SaveChangesAsync();
            return Ok(compony);
        }
        [HttpGet]
        public async Task<IActionResult> GetComponies()
        {
            var componies = await _context.Componies
                .Include(c => c.Registration)
                .Include(c => c.Branches)
                .Include(c => c.services)
                .ToListAsync();

            return Ok(componies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompony(int id)
        {
            var compony = await _context.Componies
                .Include(c => c.Registration)
                .Include(c => c.Branches)
                .Include(c => c.services)
                .FirstOrDefaultAsync(c => c.Id == id);
            if(compony == null)
                return NotFound();

            return Ok(compony);
        }
    }
}
