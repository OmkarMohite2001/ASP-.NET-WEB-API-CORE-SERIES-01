using EVChargingManagementAPI.DTOs;
using EVChargingManagementAPI.Models;
using EVChargingManagementAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EVChargingManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargingStationsController : ControllerBase
    {
        private readonly IChargingStationRepository _chargingStationRepository;

        public ChargingStationsController(IChargingStationRepository chargingStationRepository)
        {
            _chargingStationRepository = chargingStationRepository;
        }

        // CRUD Operations
        [HttpGet]
        public async Task<IActionResult> GetAllChargingStations()
        {
            var stations = await _chargingStationRepository.GetAllAsync();
            var stationDtos = stations.Select(s => new ChargingStationResponseDto
            {
                Id = s.Id,
                Name = s.Name,
                City = s.City,
                Address = s.Address,
                IsActive = s.IsActive
            }).ToList();

            return Ok(stationDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChargingStationById(int id)
        {
            var station = await _chargingStationRepository.GetByIdAsync(id);
            if (station == null)
                return NotFound(new { message = "Charging Station not found" });

            var stationDto = new ChargingStationResponseDto
            {
                Id = station.Id,
                Name = station.Name,
                City = station.City,
                Address = station.Address,
                IsActive = station.IsActive
            };

            return Ok(stationDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChargingStation([FromBody] CreateChargingStationDto createStationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var station = new ChargingStation
            {
                Name = createStationDto.Name,
                City = createStationDto.City,
                Address = createStationDto.Address,
                IsActive = createStationDto.IsActive
            };

            await _chargingStationRepository.AddAsync(station);

            var stationDto = new ChargingStationResponseDto
            {
                Id = station.Id,
                Name = station.Name,
                City = station.City,
                Address = station.Address,
                IsActive = station.IsActive
            };

            return CreatedAtAction(nameof(GetChargingStationById), new { id = station.Id }, stationDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChargingStation(int id, [FromBody] UpdateChargingStationDto updateStationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var station = await _chargingStationRepository.GetByIdAsync(id);
            if (station == null)
                return NotFound(new { message = "Charging Station not found" });

            station.Name = updateStationDto.Name;
            station.City = updateStationDto.City;
            station.Address = updateStationDto.Address;
            station.IsActive = updateStationDto.IsActive;

            _chargingStationRepository.Update(station);
            await _chargingStationRepository.SaveAsync();

            var stationDto = new ChargingStationResponseDto
            {
                Id = station.Id,
                Name = station.Name,
                City = station.City,
                Address = station.Address,
                IsActive = station.IsActive
            };

            return Ok(stationDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChargingStation(int id)
        {
            var station = await _chargingStationRepository.GetByIdAsync(id);
            if (station == null)
                return NotFound(new { message = "Charging Station not found" });

            _chargingStationRepository.Delete(station);
            await _chargingStationRepository.SaveAsync();

            return NoContent();
        }

        // Advanced queries
        [HttpGet("active/list")]
        public async Task<IActionResult> GetActiveStations()
        {
            var stations = await _chargingStationRepository.GetActiveStationsAsync();
            var stationDtos = stations.Select(s => new ChargingStationResponseDto
            {
                Id = s.Id,
                Name = s.Name,
                City = s.City,
                Address = s.Address,
                IsActive = s.IsActive
            }).ToList();

            return Ok(stationDtos);
        }

        [HttpGet("{id}/chargers")]
        public async Task<IActionResult> GetStationWithChargers(int id)
        {
            var station = await _chargingStationRepository.GetStationWithChargersAsync(id);
            if (station == null)
                return NotFound(new { message = "Charging Station not found" });

            var chargerDtos = station.Chargers?.Select(c => new ChargerResponseDto
            {
                Id = c.Id,
                ChargerCode = c.ChargerCode,
                ChargerType = c.ChargerType,
                PowerKW = c.PowerKW,
                IsAvailable = c.IsAvailable,
                ChargingStationId = c.ChargingStationId
            }).ToList() ?? new List<ChargerResponseDto>();

            var stationDto = new ChargingStationWithChargersDto
            {
                Id = station.Id,
                Name = station.Name,
                City = station.City,
                Address = station.Address,
                IsActive = station.IsActive,
                Chargers = chargerDtos
            };

            return Ok(stationDto);
        }

        [HttpGet("by-city/{city}")]
        public async Task<IActionResult> GetStationsByCity(string city)
        {
            var stations = await _chargingStationRepository.GetStationsByCityAsync(city);
            var stationDtos = stations.Select(s => new ChargingStationResponseDto
            {
                Id = s.Id,
                Name = s.Name,
                City = s.City,
                Address = s.Address,
                IsActive = s.IsActive
            }).ToList();

            return Ok(stationDtos);
        }

        [HttpGet("active-by-city/{city}")]
        public async Task<IActionResult> GetActiveStationsByCity(string city)
        {
            var stations = await _chargingStationRepository.GetActiveStationsByCityAsync(city);
            var stationDtos = stations.Select(s => new ChargingStationResponseDto
            {
                Id = s.Id,
                Name = s.Name,
                City = s.City,
                Address = s.Address,
                IsActive = s.IsActive
            }).ToList();

            return Ok(stationDtos);
        }

        [HttpGet("all-active-check")]
        public async Task<IActionResult> AreAllStationsActive()
        {
            var allActive = await _chargingStationRepository.AreAllStationsActiveAsync();

            return Ok(new
            {
                allStationsActive = allActive
            });
        }
    }
}
