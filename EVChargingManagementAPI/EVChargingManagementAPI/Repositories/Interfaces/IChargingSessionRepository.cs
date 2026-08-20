using EVChargingManagementAPI.Models;

namespace EVChargingManagementAPI.Repositories.Interfaces
{
    public interface IChargingSessionRepository : IGenericRepository<ChargingSession>
    {
        Task<List<ChargingSession>> GetSessionsByVehicleAsync(int vehicleId);
        Task<List<ChargingSession>> GetSessionsByChargerAsync(int chargerId);
        Task<List<ChargingSession>> GetSessionsByStatusAsync(string status);
        Task<ChargingSession?> GetSessionWithDetailsAsync(int id);
        Task<List<ChargingSession>> GetSessionsWithDetailsAsync();
        Task<double> GetTotalEnergyConsumedAsync();
        Task<double> GetAverageEnergyConsumedAsync();
        Task<decimal> GetTotalAmountAsync();
        Task<decimal> GetAverageAmountAsync();
        Task<decimal> GetMinimumAmountAsync();
        Task<decimal> GetMaximumAmountAsync();
        Task<List<ChargingSession>> GetSessionsBetweenDatesAsync(DateTime startDate, DateTime endDate);
    }
}
