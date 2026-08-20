using EVChargingManagementAPI.DTOs;
using EVChargingManagementAPI.Models;
using EVChargingManagementAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EVChargingManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargingSessionsController : ControllerBase
    {
        private readonly IChargingSessionRepository _chargingSessionRepository;

        public ChargingSessionsController(IChargingSessionRepository chargingSessionRepository)
        {
            _chargingSessionRepository = chargingSessionRepository;
        }

        // CRUD Operations
        [HttpGet]
        public async Task<IActionResult> GetAllChargingSessions()
        {
            var sessions = await _chargingSessionRepository.GetAllAsync();
            var sessionDtos = sessions.Select(s => new ChargingSessionResponseDto
            {
                Id = s.Id,
                VehicleId = s.VehicleId,
                ChargerId = s.ChargerId,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                EnergyConsumedKWh = s.EnergyConsumedKWh,
                Amount = s.Amount,
                Status = s.Status
            }).ToList();

            return Ok(sessionDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChargingSessionById(int id)
        {
            var session = await _chargingSessionRepository.GetByIdAsync(id);
            if (session == null)
                return NotFound(new { message = "Charging Session not found" });

            var sessionDto = new ChargingSessionResponseDto
            {
                Id = session.Id,
                VehicleId = session.VehicleId,
                ChargerId = session.ChargerId,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                EnergyConsumedKWh = session.EnergyConsumedKWh,
                Amount = session.Amount,
                Status = session.Status
            };

            return Ok(sessionDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChargingSession([FromBody] CreateChargingSessionDto createSessionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var session = new ChargingSession
            {
                VehicleId = createSessionDto.VehicleId,
                ChargerId = createSessionDto.ChargerId,
                StartTime = createSessionDto.StartTime,
                EndTime = createSessionDto.EndTime,
                EnergyConsumedKWh = createSessionDto.EnergyConsumedKWh,
                Amount = createSessionDto.Amount,
                Status = createSessionDto.Status
            };

            await _chargingSessionRepository.AddAsync(session);

            var sessionDto = new ChargingSessionResponseDto
            {
                Id = session.Id,
                VehicleId = session.VehicleId,
                ChargerId = session.ChargerId,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                EnergyConsumedKWh = session.EnergyConsumedKWh,
                Amount = session.Amount,
                Status = session.Status
            };

            return CreatedAtAction(nameof(GetChargingSessionById), new { id = session.Id }, sessionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChargingSession(int id, [FromBody] UpdateChargingSessionDto updateSessionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var session = await _chargingSessionRepository.GetByIdAsync(id);
            if (session == null)
                return NotFound(new { message = "Charging Session not found" });

            session.VehicleId = updateSessionDto.VehicleId;
            session.ChargerId = updateSessionDto.ChargerId;
            session.StartTime = updateSessionDto.StartTime;
            session.EndTime = updateSessionDto.EndTime;
            session.EnergyConsumedKWh = updateSessionDto.EnergyConsumedKWh;
            session.Amount = updateSessionDto.Amount;
            session.Status = updateSessionDto.Status;

            _chargingSessionRepository.Update(session);
            await _chargingSessionRepository.SaveAsync();

            var sessionDto = new ChargingSessionResponseDto
            {
                Id = session.Id,
                VehicleId = session.VehicleId,
                ChargerId = session.ChargerId,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                EnergyConsumedKWh = session.EnergyConsumedKWh,
                Amount = session.Amount,
                Status = session.Status
            };

            return Ok(sessionDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChargingSession(int id)
        {
            var session = await _chargingSessionRepository.GetByIdAsync(id);
            if (session == null)
                return NotFound(new { message = "Charging Session not found" });

            _chargingSessionRepository.Delete(session);
            await _chargingSessionRepository.SaveAsync();

            return NoContent();
        }

        // Advanced queries
        [HttpGet("by-vehicle/{vehicleId}")]
        public async Task<IActionResult> GetSessionsByVehicle(int vehicleId)
        {
            var sessions = await _chargingSessionRepository.GetSessionsByVehicleAsync(vehicleId);
            var sessionDtos = sessions.Select(s => new ChargingSessionResponseDto
            {
                Id = s.Id,
                VehicleId = s.VehicleId,
                ChargerId = s.ChargerId,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                EnergyConsumedKWh = s.EnergyConsumedKWh,
                Amount = s.Amount,
                Status = s.Status
            }).ToList();

            return Ok(sessionDtos);
        }

        [HttpGet("by-charger/{chargerId}")]
        public async Task<IActionResult> GetSessionsByCharger(int chargerId)
        {
            var sessions = await _chargingSessionRepository.GetSessionsByChargerAsync(chargerId);
            var sessionDtos = sessions.Select(s => new ChargingSessionResponseDto
            {
                Id = s.Id,
                VehicleId = s.VehicleId,
                ChargerId = s.ChargerId,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                EnergyConsumedKWh = s.EnergyConsumedKWh,
                Amount = s.Amount,
                Status = s.Status
            }).ToList();

            return Ok(sessionDtos);
        }

        [HttpGet("by-status/{status}")]
        public async Task<IActionResult> GetSessionsByStatus(string status)
        {
            var sessions = await _chargingSessionRepository.GetSessionsByStatusAsync(status);
            var sessionDtos = sessions.Select(s => new ChargingSessionResponseDto
            {
                Id = s.Id,
                VehicleId = s.VehicleId,
                ChargerId = s.ChargerId,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                EnergyConsumedKWh = s.EnergyConsumedKWh,
                Amount = s.Amount,
                Status = s.Status
            }).ToList();

            return Ok(sessionDtos);
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetSessionWithDetails(int id)
        {
            var session = await _chargingSessionRepository.GetSessionWithDetailsAsync(id);
            if (session == null)
                return NotFound(new { message = "Charging Session not found" });

            var vehicleDto = session.Vehicle != null ? new VehicleResponseDto
            {
                Id = session.Vehicle.Id,
                RegistrationNumber = session.Vehicle.RegistrationNumber,
                Brand = session.Vehicle.Brand,
                Model = session.Vehicle.Model,
                BatteryCapacityKWh = session.Vehicle.BatteryCapacityKWh,
                CustomerId = session.Vehicle.CustomerId
            } : null;

            var chargerDto = session.Charger != null ? new ChargerResponseDto
            {
                Id = session.Charger.Id,
                ChargerCode = session.Charger.ChargerCode,
                ChargerType = session.Charger.ChargerType,
                PowerKW = session.Charger.PowerKW,
                IsAvailable = session.Charger.IsAvailable,
                ChargingStationId = session.Charger.ChargingStationId
            } : null;

            var sessionDto = new ChargingSessionDetailsDto
            {
                Id = session.Id,
                VehicleId = session.VehicleId,
                ChargerId = session.ChargerId,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                EnergyConsumedKWh = session.EnergyConsumedKWh,
                Amount = session.Amount,
                Status = session.Status,
                Vehicle = vehicleDto,
                Charger = chargerDto
            };

            return Ok(sessionDto);
        }

        [HttpGet("list/with-details")]
        public async Task<IActionResult> GetAllSessionsWithDetails()
        {
            var sessions = await _chargingSessionRepository.GetSessionsWithDetailsAsync();
            var sessionDtos = sessions.Select(s => new ChargingSessionDetailsDto
            {
                Id = s.Id,
                VehicleId = s.VehicleId,
                ChargerId = s.ChargerId,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                EnergyConsumedKWh = s.EnergyConsumedKWh,
                Amount = s.Amount,
                Status = s.Status,
                Vehicle = s.Vehicle != null ? new VehicleResponseDto
                {
                    Id = s.Vehicle.Id,
                    RegistrationNumber = s.Vehicle.RegistrationNumber,
                    Brand = s.Vehicle.Brand,
                    Model = s.Vehicle.Model,
                    BatteryCapacityKWh = s.Vehicle.BatteryCapacityKWh,
                    CustomerId = s.Vehicle.CustomerId
                } : null,
                Charger = s.Charger != null ? new ChargerResponseDto
                {
                    Id = s.Charger.Id,
                    ChargerCode = s.Charger.ChargerCode,
                    ChargerType = s.Charger.ChargerType,
                    PowerKW = s.Charger.PowerKW,
                    IsAvailable = s.Charger.IsAvailable,
                    ChargingStationId = s.Charger.ChargingStationId
                } : null
            }).ToList();

            return Ok(sessionDtos);
        }

        [HttpGet("statistics/total-energy")]
        public async Task<IActionResult> GetTotalEnergyConsumed()
        {
            var totalEnergy = await _chargingSessionRepository.GetTotalEnergyConsumedAsync();

            return Ok(new
            {
                totalEnergyConsumedKWh = totalEnergy
            });
        }

        [HttpGet("statistics/average-energy")]
        public async Task<IActionResult> GetAverageEnergyConsumed()
        {
            var average = await _chargingSessionRepository.GetAverageEnergyConsumedAsync();

            return Ok(new
            {
                averageEnergyConsumedKWh = average
            });
        }

        [HttpGet("statistics/total-amount")]
        public async Task<IActionResult> GetTotalAmount()
        {
            var totalAmount = await _chargingSessionRepository.GetTotalAmountAsync();

            return Ok(new
            {
                totalAmount = totalAmount
            });
        }

        [HttpGet("statistics/average-amount")]
        public async Task<IActionResult> GetAverageAmount()
        {
            var average = await _chargingSessionRepository.GetAverageAmountAsync();

            return Ok(new
            {
                averageAmount = average
            });
        }

        [HttpGet("statistics/minimum-amount")]
        public async Task<IActionResult> GetMinimumAmount()
        {
            var minimum = await _chargingSessionRepository.GetMinimumAmountAsync();

            return Ok(new
            {
                minimumAmount = minimum
            });
        }

        [HttpGet("statistics/maximum-amount")]
        public async Task<IActionResult> GetMaximumAmount()
        {
            var maximum = await _chargingSessionRepository.GetMaximumAmountAsync();

            return Ok(new
            {
                maximumAmount = maximum
            });
        }

        [HttpGet("between-dates")]
        public async Task<IActionResult> GetSessionsBetweenDates([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var sessions = await _chargingSessionRepository.GetSessionsBetweenDatesAsync(startDate, endDate);
            var sessionDtos = sessions.Select(s => new ChargingSessionResponseDto
            {
                Id = s.Id,
                VehicleId = s.VehicleId,
                ChargerId = s.ChargerId,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                EnergyConsumedKWh = s.EnergyConsumedKWh,
                Amount = s.Amount,
                Status = s.Status
            }).ToList();

            return Ok(sessionDtos);
        }
    }
}
