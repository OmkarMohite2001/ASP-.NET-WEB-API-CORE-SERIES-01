using EVChargingManagementAPI.Data;
using EVChargingManagementAPI.Models;
using EVChargingManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EVChargingManagementAPI.Repositories
{
    public class ChargingStationRepository : GenericRepository<ChargingStation>, IChargingStationRepository
    {
        public ChargingStationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<ChargingStation>> GetActiveStationsAsync()
        {
            return await _context.ChargingStations
                .Where(s => s.IsActive)
                .ToListAsync();
        }

        public async Task<ChargingStation?> GetStationWithChargersAsync(int id)
        {
            return await _context.ChargingStations
                .Include(s => s.Chargers)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<ChargingStation>> GetStationsByCityAsync(string city)
        {
            return await _context.ChargingStations
                .Where(s => s.City == city)
                .ToListAsync();
        }

        public async Task<List<ChargingStation>> GetActiveStationsByCityAsync(string city)
        {
            return await _context.ChargingStations
                .Where(s => s.IsActive && s.City == city)
                .ToListAsync();
        }

        public async Task<bool> AreAllStationsActiveAsync()
        {
            return await _context.ChargingStations
                .AllAsync(s => s.IsActive);
        }
    }
}
