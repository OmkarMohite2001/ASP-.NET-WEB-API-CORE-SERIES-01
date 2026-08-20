using EVChargingManagementAPI.Models;

namespace EVChargingManagementAPI.Repositories.Interfaces
{
    public interface IChargerRepository : IGenericRepository<Charger>
    {
        Task<List<Charger>> GetChargersByStationAsync(int stationId);
        Task<List<Charger>> GetAvailableChargersAsync();
        Task<List<Charger>> GetAvailableChargersByStationAsync(int stationId);
        Task<List<Charger>> GetChargersByTypeAsync(string chargerType);
        Task<List<Charger>> GetChargersByPowerAsync(double minPowerKW);
        Task<Charger?> GetChargerWithSessionsAsync(int id);
    }
}
