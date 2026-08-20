using EVChargingManagementAPI.DTOs;
using EVChargingManagementAPI.Models;
using EVChargingManagementAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EVChargingManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehiclesController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        // CRUD Operations
        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            var vehicles = await _vehicleRepository.GetAllAsync();
            var vehicleDtos = vehicles.Select(v => new VehicleResponseDto
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Brand = v.Brand,
                Model = v.Model,
                BatteryCapacityKWh = v.BatteryCapacityKWh,
                CustomerId = v.CustomerId
            }).ToList();

            return Ok(vehicleDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
                return NotFound(new { message = "Vehicle not found" });

            var vehicleDto = new VehicleResponseDto
            {
                Id = vehicle.Id,
                RegistrationNumber = vehicle.RegistrationNumber,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                BatteryCapacityKWh = vehicle.BatteryCapacityKWh,
                CustomerId = vehicle.CustomerId
            };

            return Ok(vehicleDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleDto createVehicleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = new Vehicle
            {
                RegistrationNumber = createVehicleDto.RegistrationNumber,
                Brand = createVehicleDto.Brand,
                Model = createVehicleDto.Model,
                BatteryCapacityKWh = createVehicleDto.BatteryCapacityKWh,
                CustomerId = createVehicleDto.CustomerId
            };

            await _vehicleRepository.AddAsync(vehicle);

            var vehicleDto = new VehicleResponseDto
            {
                Id = vehicle.Id,
                RegistrationNumber = vehicle.RegistrationNumber,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                BatteryCapacityKWh = vehicle.BatteryCapacityKWh,
                CustomerId = vehicle.CustomerId
            };

            return CreatedAtAction(nameof(GetVehicleById), new { id = vehicle.Id }, vehicleDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] UpdateVehicleDto updateVehicleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
                return NotFound(new { message = "Vehicle not found" });

            vehicle.RegistrationNumber = updateVehicleDto.RegistrationNumber;
            vehicle.Brand = updateVehicleDto.Brand;
            vehicle.Model = updateVehicleDto.Model;
            vehicle.BatteryCapacityKWh = updateVehicleDto.BatteryCapacityKWh;
            vehicle.CustomerId = updateVehicleDto.CustomerId;

            _vehicleRepository.Update(vehicle);
            await _vehicleRepository.SaveAsync();

            var vehicleDto = new VehicleResponseDto
            {
                Id = vehicle.Id,
                RegistrationNumber = vehicle.RegistrationNumber,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                BatteryCapacityKWh = vehicle.BatteryCapacityKWh,
                CustomerId = vehicle.CustomerId
            };

            return Ok(vehicleDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
                return NotFound(new { message = "Vehicle not found" });

            _vehicleRepository.Delete(vehicle);
            await _vehicleRepository.SaveAsync();

            return NoContent();
        }

        // Advanced queries
        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetVehiclesByCustomer(int customerId)
        {
            var vehicles = await _vehicleRepository.GetVehiclesByCustomerAsync(customerId);
            var vehicleDtos = vehicles.Select(v => new VehicleResponseDto
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Brand = v.Brand,
                Model = v.Model,
                BatteryCapacityKWh = v.BatteryCapacityKWh,
                CustomerId = v.CustomerId
            }).ToList();

            return Ok(vehicleDtos);
        }

        [HttpGet("high-capacity")]
        public async Task<IActionResult> GetHighCapacityVehicles()
        {
            var vehicles = await _vehicleRepository.GetHighCapacityVehiclesAsync();
            var vehicleDtos = vehicles.Select(v => new VehicleResponseDto
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Brand = v.Brand,
                Model = v.Model,
                BatteryCapacityKWh = v.BatteryCapacityKWh,
                CustomerId = v.CustomerId
            }).ToList();

            return Ok(vehicleDtos);
        }

        [HttpGet("by-brand/{brand}")]
        public async Task<IActionResult> GetVehiclesByBrand(string brand)
        {
            var vehicles = await _vehicleRepository.GetVehiclesByBrandAsync(brand);
            var vehicleDtos = vehicles.Select(v => new VehicleResponseDto
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Brand = v.Brand,
                Model = v.Model,
                BatteryCapacityKWh = v.BatteryCapacityKWh,
                CustomerId = v.CustomerId
            }).ToList();

            return Ok(vehicleDtos);
        }

        [HttpGet("premium-tesla")]
        public async Task<IActionResult> GetPremiumTeslaVehicles()
        {
            var vehicles = await _vehicleRepository.GetPremiumTeslaVehiclesAsync();
            var vehicleDtos = vehicles.Select(v => new VehicleResponseDto
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Brand = v.Brand,
                Model = v.Model,
                BatteryCapacityKWh = v.BatteryCapacityKWh,
                CustomerId = v.CustomerId
            }).ToList();

            return Ok(vehicleDtos);
        }

        [HttpGet("tesla-or-tata")]
        public async Task<IActionResult> GetTeslaORTataVehicles()
        {
            var vehicles = await _vehicleRepository.GetVehiclesByMultipleBrandsAsync("Tesla", "Tata");
            var vehicleDtos = vehicles.Select(v => new VehicleResponseDto
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Brand = v.Brand,
                Model = v.Model,
                BatteryCapacityKWh = v.BatteryCapacityKWh,
                CustomerId = v.CustomerId
            }).ToList();

            return Ok(vehicleDtos);
        }

        [HttpGet("registration-starts/{prefix}")]
        public async Task<IActionResult> GetVehiclesByRegistrationPrefix(string prefix)
        {
            var vehicles = await _vehicleRepository.GetVehiclesByRegistrationPrefixAsync(prefix);
            var vehicleDtos = vehicles.Select(v => new VehicleResponseDto
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Brand = v.Brand,
                Model = v.Model,
                BatteryCapacityKWh = v.BatteryCapacityKWh,
                CustomerId = v.CustomerId
            }).ToList();

            return Ok(vehicleDtos);
        }

        [HttpGet("registration-ends/{suffix}")]
        public async Task<IActionResult> GetVehiclesByRegistrationSuffix(string suffix)
        {
            var vehicles = await _vehicleRepository.GetVehiclesByRegistrationSuffixAsync(suffix);
            var vehicleDtos = vehicles.Select(v => new VehicleResponseDto
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Brand = v.Brand,
                Model = v.Model,
                BatteryCapacityKWh = v.BatteryCapacityKWh,
                CustomerId = v.CustomerId
            }).ToList();

            return Ok(vehicleDtos);
        }

        [HttpGet("{id}/sessions")]
        public async Task<IActionResult> GetVehicleWithSessions(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicleWithSessionsAsync(id);
            if (vehicle == null)
                return NotFound(new { message = "Vehicle not found" });

            var sessionDtos = vehicle.ChargingSessions?.Select(s => new ChargingSessionResponseDto
            {
                Id = s.Id,
                VehicleId = s.VehicleId,
                ChargerId = s.ChargerId,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                EnergyConsumedKWh = s.EnergyConsumedKWh,
                Amount = s.Amount,
                Status = s.Status
            }).ToList() ?? new List<ChargingSessionResponseDto>();

            var vehicleDto = new VehicleWithSessionsDto
            {
                Id = vehicle.Id,
                RegistrationNumber = vehicle.RegistrationNumber,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                BatteryCapacityKWh = vehicle.BatteryCapacityKWh,
                CustomerId = vehicle.CustomerId,
                ChargingSessions = sessionDtos
            };

            return Ok(vehicleDto);
        }
    }
}
