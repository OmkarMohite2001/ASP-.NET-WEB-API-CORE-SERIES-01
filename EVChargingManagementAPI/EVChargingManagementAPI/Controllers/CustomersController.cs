using EVChargingManagementAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EVChargingManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("selected-cities")]
        public async Task<IActionResult>GetCustomersFromSelectedCities()
        {
            var cities = new List<string>
            {
                "Pune",
                "Mumbai",
                "Nashik"
            };

            var customers = await _context.Customers
                .Where(c => cities.Contains(c.City))
                .ToListAsync();

            return Ok(customers);
        }
        [HttpGet("tesla-exists")]
        public async Task<IActionResult> TeslaExists()
        {
            var exists = await _context.Vehicles
                .AnyAsync(v => v.Brand == "Tasla");

            return Ok(new
            {
                exists
            });
        }
        [HttpGet("all-active")]
        public async Task<IActionResult> AreAllStationsActive()
        {
            var result = await _context.ChargingStations
                .AllAsync(s => s.IsActive);

            return Ok(new
            {
                allStationsActive = result
            });
        }
        [HttpGet("count")]
        public async Task<IActionResult> GetVehicleCount()
        {
            var count = await _context.Vehicles
                .CountAsync();

            return Ok(new
            {
                totalVehicles = count
            });
        }
        [HttpGet("high-capacity-count")]
        public async Task<IActionResult> GetHighCapacityVehicleCount()
        {
            var count = await _context.Vehicles
                .CountAsync(v =>
                    v.BatteryCapacityKWh > 60);

            return Ok(new
            {
                highCapacityVehicles = count
            });
        }
        [HttpGet("total-energy")]
        public async Task<IActionResult> GetTotalEnergy()
        {
            var totalEnergy =
                await _context.ChargingSessions
                .SumAsync(s => s.EnergyConsumedKWh);

            return Ok(new
            {
                totalEnergy
            });
        }
        [HttpGet("average-energy")]
        public async Task<IActionResult> GetAverageEnergy()
        {
            var average =
                await _context.ChargingSessions
                .AverageAsync(s =>
                    s.EnergyConsumedKWh);

            return Ok(new
            {
                averageEnergy = average
            });
        }
        [HttpGet("minimum-session-amount")]
        public async Task<IActionResult>
    GetMinimumSessionAmount()
        {
            var minimum =
                await _context.ChargingSessions
                .MinAsync(s => s.Amount);

            return Ok(new
            {
                minimumAmount = minimum
            });
        }
        [HttpGet("maximum-session-amount")]
        public async Task<IActionResult>
    GetMaximumSessionAmount()
        {
            var maximum =
                await _context.ChargingSessions
                .MaxAsync(s => s.Amount);

            return Ok(new
            {
                maximumAmount = maximum
            });
        }
    }
}
