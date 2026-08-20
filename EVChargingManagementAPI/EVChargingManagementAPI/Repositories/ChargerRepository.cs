using EVChargingManagementAPI.Data;
using EVChargingManagementAPI.Models;
using EVChargingManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EVChargingManagementAPI.Repositories
{
    public class ChargerRepository : GenericRepository<Charger>, IChargerRepository
    {
        public ChargerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Charger>> GetChargersByStationAsync(int stationId)
        {
            return await _context.Chargers
                .Where(c => c.ChargingStationId == stationId)
                .ToListAsync();
        }

        public async Task<List<Charger>> GetAvailableChargersAsync()
        {
            return await _context.Chargers
                .Where(c => c.IsAvailable)
                .ToListAsync();
        }

        public async Task<List<Charger>> GetAvailableChargersByStationAsync(int stationId)
        {
            return await _context.Chargers
                .Where(c => c.ChargingStationId == stationId && c.IsAvailable)
                .ToListAsync();
        }

        public async Task<List<Charger>> GetChargersByTypeAsync(string chargerType)
        {
            return await _context.Chargers
                .Where(c => c.ChargerType == chargerType)
                .ToListAsync();
        }

        public async Task<List<Charger>> GetChargersByPowerAsync(double minPowerKW)
        {
            return await _context.Chargers
                .Where(c => c.PowerKW >= minPowerKW)
                .ToListAsync();
        }

        public async Task<Charger?> GetChargerWithSessionsAsync(int id)
        {
            return await _context.Chargers
                .Include(c => c.ChargingSessions)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
