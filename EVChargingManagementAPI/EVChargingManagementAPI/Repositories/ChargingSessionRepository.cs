using EVChargingManagementAPI.Data;
using EVChargingManagementAPI.Models;
using EVChargingManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EVChargingManagementAPI.Repositories
{
    public class ChargingSessionRepository : GenericRepository<ChargingSession>, IChargingSessionRepository
    {
        public ChargingSessionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<ChargingSession>> GetSessionsByVehicleAsync(int vehicleId)
        {
            return await _context.ChargingSessions
                .Where(s => s.VehicleId == vehicleId)
                .ToListAsync();
        }

        public async Task<List<ChargingSession>> GetSessionsByChargerAsync(int chargerId)
        {
            return await _context.ChargingSessions
                .Where(s => s.ChargerId == chargerId)
                .ToListAsync();
        }

        public async Task<List<ChargingSession>> GetSessionsByStatusAsync(string status)
        {
            return await _context.ChargingSessions
                .Where(s => s.Status == status)
                .ToListAsync();
        }

        public async Task<ChargingSession?> GetSessionWithDetailsAsync(int id)
        {
            return await _context.ChargingSessions
                .Include(s => s.Vehicle)
                .Include(s => s.Charger)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<ChargingSession>> GetSessionsWithDetailsAsync()
        {
            return await _context.ChargingSessions
                .Include(s => s.Vehicle)
                .Include(s => s.Charger)
                .ToListAsync();
        }

        public async Task<double> GetTotalEnergyConsumedAsync()
        {
            return await _context.ChargingSessions
                .SumAsync(s => s.EnergyConsumedKWh);
        }

        public async Task<double> GetAverageEnergyConsumedAsync()
        {
            var count = await _context.ChargingSessions.CountAsync();
            if (count == 0) return 0;

            return await _context.ChargingSessions
                .AverageAsync(s => s.EnergyConsumedKWh);
        }

        public async Task<decimal> GetTotalAmountAsync()
        {
            return await _context.ChargingSessions
                .SumAsync(s => s.Amount);
        }

        public async Task<decimal> GetAverageAmountAsync()
        {
            var count = await _context.ChargingSessions.CountAsync();
            if (count == 0) return 0;

            return await _context.ChargingSessions
                .AverageAsync(s => s.Amount);
        }

        public async Task<decimal> GetMinimumAmountAsync()
        {
            var sessions = await _context.ChargingSessions.ToListAsync();
            return sessions.Count > 0 ? sessions.Min(s => s.Amount) : 0;
        }

        public async Task<decimal> GetMaximumAmountAsync()
        {
            var sessions = await _context.ChargingSessions.ToListAsync();
            return sessions.Count > 0 ? sessions.Max(s => s.Amount) : 0;
        }

        public async Task<List<ChargingSession>> GetSessionsBetweenDatesAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.ChargingSessions
                .Where(s => s.StartTime >= startDate && s.EndTime <= endDate)
                .ToListAsync();
        }
    }
}
