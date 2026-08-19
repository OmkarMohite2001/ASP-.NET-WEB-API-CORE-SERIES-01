using EVChargingManagementAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EVChargingManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public VehiclesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("high-capacity")]
        public async Task<IActionResult> GetHighCapacityVehicles()
        {
            var vehicles = await _context.Vehicles
                .Where(v => v.BatteryCapacityKWh > 60)
                .ToListAsync();
            return Ok(vehicles);
        }
        [HttpGet("by-brand/{brand}")]
        public async Task<IActionResult> GetVehiclesByBrand(string brand)
        {
            var vehicles = await _context.Vehicles
                .Where(v => v.Brand == brand)
                .ToListAsync();

            return Ok(vehicles);
        }
        [HttpGet("premium-tesla")]
        public async Task<IActionResult> GetPremiumTeslaVehicles()
        {
            var vehicles = await _context.Vehicles
                .Where(v =>
                    v.Brand == "Tesla" &&
                    v.BatteryCapacityKWh > 80)
                .ToListAsync();

            return Ok(vehicles);
        }
        [HttpGet("tesla-or-tata")]
        public async Task<IActionResult> GetTeslaORTataVehicles()
        {
            var vehicles = await _context.Vehicles
                .Where(v =>
                    v.Brand == "Tesla" ||
                    v.Brand == "Tata")
                .ToListAsync();

            return Ok(vehicles);
        }
        [HttpGet("registration-starts/{prefix}")]public async Task<IActionResult> GetVehiclesByRegistrationPrefix(string prefix)
        {
            var vehicles = await _context.Vehicles
                .Where(v => v.RegistrationNumber.StartsWith(prefix))
                .ToListAsync();

            return Ok(vehicles);
        }
        [HttpGet("registration-ends/{suffix}")]public async Task<IActionResult> GetVehiclesByRegistrationSuffix(string suffix)
        {
            var vehicles = await _context.Vehicles
                .Where(v => v.RegistrationNumber.EndsWith(suffix))
                .ToListAsync();

            return Ok(vehicles);
        }
    }
}
