using EVChargingManagementAPI.Data;
using EVChargingManagementAPI.Models;
using EVChargingManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EVChargingManagementAPI.Repositories
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Vehicle>> GetVehiclesByCustomerAsync(int customerId)
        {
            return await _context.Vehicles
                .Where(v => v.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<List<Vehicle>> GetHighCapacityVehiclesAsync(double capacityThreshold = 60)
        {
            return await _context.Vehicles
                .Where(v => v.BatteryCapacityKWh > capacityThreshold)
                .ToListAsync();
        }

        public async Task<List<Vehicle>> GetVehiclesByBrandAsync(string brand)
        {
            return await _context.Vehicles
                .Where(v => v.Brand == brand)
                .ToListAsync();
        }

        public async Task<List<Vehicle>> GetVehiclesByRegistrationPrefixAsync(string prefix)
        {
            return await _context.Vehicles
                .Where(v => v.RegistrationNumber.StartsWith(prefix))
                .ToListAsync();
        }

        public async Task<List<Vehicle>> GetVehiclesByRegistrationSuffixAsync(string suffix)
        {
            return await _context.Vehicles
                .Where(v => v.RegistrationNumber.EndsWith(suffix))
                .ToListAsync();
        }

        public async Task<List<Vehicle>> GetPremiumTeslaVehiclesAsync(double capacityThreshold = 80)
        {
            return await _context.Vehicles
                .Where(v => v.Brand == "Tesla" && v.BatteryCapacityKWh > capacityThreshold)
                .ToListAsync();
        }

        public async Task<List<Vehicle>> GetVehiclesByMultipleBrandsAsync(params string[] brands)
        {
            return await _context.Vehicles
                .Where(v => brands.Contains(v.Brand))
                .ToListAsync();
        }

        public async Task<Vehicle?> GetVehicleWithSessionsAsync(int id)
        {
            return await _context.Vehicles
                .Include(v => v.ChargingSessions)
                .FirstOrDefaultAsync(v => v.Id == id);
        }
    }
}
