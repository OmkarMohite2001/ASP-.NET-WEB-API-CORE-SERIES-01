using EVChargingManagementAPI.DTOs;
using EVChargingManagementAPI.Models;
using EVChargingManagementAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EVChargingManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargersController : ControllerBase
    {
        private readonly IChargerRepository _chargerRepository;

        public ChargersController(IChargerRepository chargerRepository)
        {
            _chargerRepository = chargerRepository;
        }

        // CRUD Operations
        [HttpGet]
        public async Task<IActionResult> GetAllChargers()
        {
            var chargers = await _chargerRepository.GetAllAsync();
            var chargerDtos = chargers.Select(c => new ChargerResponseDto
            {
                Id = c.Id,
                ChargerCode = c.ChargerCode,
                ChargerType = c.ChargerType,
                PowerKW = c.PowerKW,
                IsAvailable = c.IsAvailable,
                ChargingStationId = c.ChargingStationId
            }).ToList();

            return Ok(chargerDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChargerById(int id)
        {
            var charger = await _chargerRepository.GetByIdAsync(id);
            if (charger == null)
                return NotFound(new { message = "Charger not found" });

            var chargerDto = new ChargerResponseDto
            {
                Id = charger.Id,
                ChargerCode = charger.ChargerCode,
                ChargerType = charger.ChargerType,
                PowerKW = charger.PowerKW,
                IsAvailable = charger.IsAvailable,
                ChargingStationId = charger.ChargingStationId
            };

            return Ok(chargerDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCharger([FromBody] CreateChargerDto createChargerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var charger = new Charger
            {
                ChargerCode = createChargerDto.ChargerCode,
                ChargerType = createChargerDto.ChargerType,
                PowerKW = createChargerDto.PowerKW,
                IsAvailable = createChargerDto.IsAvailable,
                ChargingStationId = createChargerDto.ChargingStationId
            };

            await _chargerRepository.AddAsync(charger);

            var chargerDto = new ChargerResponseDto
            {
                Id = charger.Id,
                ChargerCode = charger.ChargerCode,
                ChargerType = charger.ChargerType,
                PowerKW = charger.PowerKW,
                IsAvailable = charger.IsAvailable,
                ChargingStationId = charger.ChargingStationId
            };

            return CreatedAtAction(nameof(GetChargerById), new { id = charger.Id }, chargerDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharger(int id, [FromBody] UpdateChargerDto updateChargerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var charger = await _chargerRepository.GetByIdAsync(id);
            if (charger == null)
                return NotFound(new { message = "Charger not found" });

            charger.ChargerCode = updateChargerDto.ChargerCode;
            charger.ChargerType = updateChargerDto.ChargerType;
            charger.PowerKW = updateChargerDto.PowerKW;
            charger.IsAvailable = updateChargerDto.IsAvailable;
            charger.ChargingStationId = updateChargerDto.ChargingStationId;

            _chargerRepository.Update(charger);
            await _chargerRepository.SaveAsync();

            var chargerDto = new ChargerResponseDto
            {
                Id = charger.Id,
                ChargerCode = charger.ChargerCode,
                ChargerType = charger.ChargerType,
                PowerKW = charger.PowerKW,
                IsAvailable = charger.IsAvailable,
                ChargingStationId = charger.ChargingStationId
            };

            return Ok(chargerDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharger(int id)
        {
            var charger = await _chargerRepository.GetByIdAsync(id);
            if (charger == null)
                return NotFound(new { message = "Charger not found" });

            _chargerRepository.Delete(charger);
            await _chargerRepository.SaveAsync();

            return NoContent();
        }

        // Advanced queries
        [HttpGet("by-station/{stationId}")]
        public async Task<IActionResult> GetChargersByStation(int stationId)
        {
            var chargers = await _chargerRepository.GetChargersByStationAsync(stationId);
            var chargerDtos = chargers.Select(c => new ChargerResponseDto
            {
                Id = c.Id,
                ChargerCode = c.ChargerCode,
                ChargerType = c.ChargerType,
                PowerKW = c.PowerKW,
                IsAvailable = c.IsAvailable,
                ChargingStationId = c.ChargingStationId
            }).ToList();

            return Ok(chargerDtos);
        }

        [HttpGet("available/list")]
        public async Task<IActionResult> GetAvailableChargers()
        {
            var chargers = await _chargerRepository.GetAvailableChargersAsync();
            var chargerDtos = chargers.Select(c => new ChargerResponseDto
            {
                Id = c.Id,
                ChargerCode = c.ChargerCode,
                ChargerType = c.ChargerType,
                PowerKW = c.PowerKW,
                IsAvailable = c.IsAvailable,
                ChargingStationId = c.ChargingStationId
            }).ToList();

            return Ok(chargerDtos);
        }

        [HttpGet("available/by-station/{stationId}")]
        public async Task<IActionResult> GetAvailableChargersByStation(int stationId)
        {
            var chargers = await _chargerRepository.GetAvailableChargersByStationAsync(stationId);
            var chargerDtos = chargers.Select(c => new ChargerResponseDto
            {
                Id = c.Id,
                ChargerCode = c.ChargerCode,
                ChargerType = c.ChargerType,
                PowerKW = c.PowerKW,
                IsAvailable = c.IsAvailable,
                ChargingStationId = c.ChargingStationId
            }).ToList();

            return Ok(chargerDtos);
        }

        [HttpGet("by-type/{chargerType}")]
        public async Task<IActionResult> GetChargersByType(string chargerType)
        {
            var chargers = await _chargerRepository.GetChargersByTypeAsync(chargerType);
            var chargerDtos = chargers.Select(c => new ChargerResponseDto
            {
                Id = c.Id,
                ChargerCode = c.ChargerCode,
                ChargerType = c.ChargerType,
                PowerKW = c.PowerKW,
                IsAvailable = c.IsAvailable,
                ChargingStationId = c.ChargingStationId
            }).ToList();

            return Ok(chargerDtos);
        }

        [HttpGet("by-power/{minPowerKW}")]
        public async Task<IActionResult> GetChargersByPower(double minPowerKW)
        {
            var chargers = await _chargerRepository.GetChargersByPowerAsync(minPowerKW);
            var chargerDtos = chargers.Select(c => new ChargerResponseDto
            {
                Id = c.Id,
                ChargerCode = c.ChargerCode,
                ChargerType = c.ChargerType,
                PowerKW = c.PowerKW,
                IsAvailable = c.IsAvailable,
                ChargingStationId = c.ChargingStationId
            }).ToList();

            return Ok(chargerDtos);
        }

        [HttpGet("{id}/sessions")]
        public async Task<IActionResult> GetChargerWithSessions(int id)
        {
            var charger = await _chargerRepository.GetChargerWithSessionsAsync(id);
            if (charger == null)
                return NotFound(new { message = "Charger not found" });

            var sessionDtos = charger.ChargingSessions?.Select(s => new ChargingSessionResponseDto
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

            var chargerDto = new ChargerWithSessionsDto
            {
                Id = charger.Id,
                ChargerCode = charger.ChargerCode,
                ChargerType = charger.ChargerType,
                PowerKW = charger.PowerKW,
                IsAvailable = charger.IsAvailable,
                ChargingStationId = charger.ChargingStationId,
                ChargingSessions = sessionDtos
            };

            return Ok(chargerDto);
        }
    }
}
