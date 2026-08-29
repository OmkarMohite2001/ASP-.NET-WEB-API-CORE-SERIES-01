using EVChargingManagementAPI.Data;
using EVChargingManagementAPI.DTOs;
using EVChargingManagementAPI.Models;
using EVChargingManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EVChargingManagementAPI.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Customer>> GetCustomersFromSelectedCitiesRepo()
        {
            var cities = new List<string>
            {
                "Pune",
                "Mumbai",
                "Nashik"
            };
            return await _context.Customers
                .Where(c => cities.Contains(c.City))
                .ToListAsync();
        }

        public async Task<bool> AreAllStationPresents()
        {
            return await _context.Vehicles
                .AnyAsync(v => v.Brand == "Tesla");
        }

        public async Task<Customer?> GetCustomerWithVehiclesAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.Vehicles)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Customer>> GetActiveCustomersAsync()
        {
            return await _context.Customers
                .Where(c => c.IsActive)
                .ToListAsync();
        }
        public async Task<(List<Customer>customers,int TotalRecords)> GetCustomersAsync(CustomerQueryDto query)
        {
            var customersQuery = _context.Customers.AsQueryable();
            //Filtering
            if(!string.IsNullOrWhiteSpace(query.City))
            {
                customersQuery = customersQuery
                    .Where(c => c.City == query.City);
            }
            if(query.IsActive.HasValue)
            {
                customersQuery = customersQuery
                    .Where(c => c.IsActive == query.IsActive.Value);
            }

            // Searching
            if(!string.IsNullOrWhiteSpace(query.Search))
            {
                customersQuery = customersQuery.Where(c=>
                c.FullName.Contains(query.Search)||c.Email.Contains(query.Search)|| c.City.Contains(query.Search));
            }

            //Total records after filtering / searching
            var totalRecords = await customersQuery.CountAsync();

            //sorting
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("FullName",
                    StringComparison.OrdinalIgnoreCase))
                {
                    customersQuery = query.SortOrder.Equals("desc",
                        StringComparison.OrdinalIgnoreCase)
                        ? customersQuery.OrderByDescending(c => c.FullName)
                        : customersQuery.OrderBy(c => c.FullName);
                }
                else if (query.SortBy.Equals("Email",
                    StringComparison.OrdinalIgnoreCase))
                {
                    customersQuery = query.SortOrder.Equals("desc",
                        StringComparison.OrdinalIgnoreCase)
                        ? customersQuery.OrderByDescending(c => c.Email)
                        : customersQuery.OrderBy(c => c.Email);
                }
                else if (query.SortBy.Equals("City",
                    StringComparison.OrdinalIgnoreCase))
                {
                    customersQuery = query.SortOrder.Equals("desc",
                        StringComparison.OrdinalIgnoreCase)
                        ? customersQuery.OrderByDescending(c => c.City)
                        : customersQuery.OrderBy(c => c.City);
                }
                else
                {
                    customersQuery = customersQuery.OrderBy(c => c.Id);
                }
            }
            else
            {
                customersQuery = customersQuery.OrderBy(c => c.Id);
            }

            // Pagination
            var skip = (query.PageNumber - 1) * query.PageSize;

            var customers = await customersQuery
                .Skip(skip)
                .Take(query.PageSize)
                .ToListAsync();

            return (customers, totalRecords);
        }
    }
}

