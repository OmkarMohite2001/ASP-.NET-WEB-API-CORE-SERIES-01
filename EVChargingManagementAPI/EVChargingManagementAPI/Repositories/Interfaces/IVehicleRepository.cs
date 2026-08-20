using EVChargingManagementAPI.Models;

namespace EVChargingManagementAPI.Repositories.Interfaces
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {
        Task<List<Vehicle>> GetVehiclesByCustomerAsync(int customerId);
        Task<List<Vehicle>> GetHighCapacityVehiclesAsync(double capacityThreshold = 60);
        Task<List<Vehicle>> GetVehiclesByBrandAsync(string brand);
        Task<List<Vehicle>> GetVehiclesByRegistrationPrefixAsync(string prefix);
        Task<List<Vehicle>> GetVehiclesByRegistrationSuffixAsync(string suffix);
        Task<List<Vehicle>> GetPremiumTeslaVehiclesAsync(double capacityThreshold = 80);
        Task<List<Vehicle>> GetVehiclesByMultipleBrandsAsync(params string[] brands);
        Task<Vehicle?> GetVehicleWithSessionsAsync(int id);
    }
}
