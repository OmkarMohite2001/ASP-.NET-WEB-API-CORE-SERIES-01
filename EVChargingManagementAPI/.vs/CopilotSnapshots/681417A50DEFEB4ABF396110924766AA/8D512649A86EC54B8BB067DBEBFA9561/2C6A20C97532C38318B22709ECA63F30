using EVChargingManagementAPI.Data;
using EVChargingManagementAPI.Models;
using EVChargingManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EVChargingManagementAPI.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .ToListAsync();
        }
        public async Task<List<Customer>> GetCustomersFromSelectedCitiesRepo()
        {
            var cities = new List<string>
            {
                "Pune",
                "Mumbai",
                "Nashik"
            };
            return  await _context.Customers
                .Where(c => cities.Contains(c.City))
                .ToListAsync();
        }
        public async Task<bool> AreAllStationPresents()
        {
            return await _context.Vehicles
                .AnyAsync(v => v.Brand == "Tasla");
        }
    }
}
