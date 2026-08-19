using ComponyRegistrationAPI.Data;
using ComponyRegistrationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComponyRegistrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponyServicesController : ControllerBase
    {
        public readonly AppDbContext? _context;
        public ComponyServicesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateService(Service service)
        {
            _context.services.Add(service);
            await _context.SaveChangesAsync();
            return Ok(service);
        }
        [HttpPost("{companyId}/services")]
        public async Task<IActionResult> AddServicesToCompany(int companyId,List<int> serviceIds)
        {
            var company = await _context.Componies
                .Include(c => c.services)
                .FirstOrDefaultAsync(c => c.Id == companyId);

            if (company == null)
            {
                return NotFound("Company not found.");
            }

            var services = await _context.services
                .Where(s => serviceIds.Contains(s.Id))
                .ToListAsync();

            foreach (var service in services)
            {
                if (!company.services.Any(s => s.Id == service.Id))
                {
                    company.services.Add(service);
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Services assigned successfully.",
                companyId = companyId,
                serviceIds = serviceIds
            });
        }
    }
}
