using EVChargingManagementAPI.Models;

namespace EVChargingManagementAPI.Repositories.Interfaces
{
    public interface IChargingStationRepository : IGenericRepository<ChargingStation>
    {
        Task<List<ChargingStation>> GetActiveStationsAsync();
        Task<ChargingStation?> GetStationWithChargersAsync(int id);
        Task<List<ChargingStation>> GetStationsByCityAsync(string city);
        Task<List<ChargingStation>> GetActiveStationsByCityAsync(string city);
        Task<bool> AreAllStationsActiveAsync();
    }
}
