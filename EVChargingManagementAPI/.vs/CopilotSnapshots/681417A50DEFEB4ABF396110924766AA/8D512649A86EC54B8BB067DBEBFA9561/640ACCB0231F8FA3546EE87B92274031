using EVChargingManagementAPI.Data;
using EVChargingManagementAPI.Repositories.Interfaces;
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
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(AppDbContext context,ICustomerRepository repository)
        {
            _context = context;
            _customerRepository = repository;
        }
        [HttpGet("selected-cities")]
        public async Task<IActionResult>GetCustomersFromSelectedCities()
        {
           var customers = await _customerRepository.GetCustomersFromSelectedCitiesRepo();

            return Ok(customers);
        }
        [HttpGet("tesla-exists")]
        public async Task<IActionResult> TeslaExists()
        {
            var exists = await _customerRepository.AreAllStationPresents();

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
        [HttpGet]
        public async Task<IActionResult> GetCustomer()
        {
            var customers = await _customerRepository.GetAllAsync();
            return Ok(customers);
        }
    }
}
