using EVChargingManagementAPI.DTOs;
using EVChargingManagementAPI.Models;

namespace EVChargingManagementAPI.Repositories.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<List<Customer>> GetCustomersFromSelectedCitiesRepo();
        Task<bool> AreAllStationPresents();
        Task<Customer?> GetCustomerWithVehiclesAsync(int id);
        Task<List<Customer>> GetActiveCustomersAsync();
        Task<(List<Customer> customers, int TotalRecords)> GetCustomersAsync(CustomerQueryDto query);
    }
}
