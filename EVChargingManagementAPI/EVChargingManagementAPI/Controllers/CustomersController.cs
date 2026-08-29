using AutoMapper;
using EVChargingManagementAPI.DTOs;
using EVChargingManagementAPI.Models;
using EVChargingManagementAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EVChargingManagementAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IChargingSessionRepository _chargingSessionRepository;
        private readonly IMapper _mapper;
        public CustomersController(ICustomerRepository customerRepository, IChargingSessionRepository chargingSessionRepository,IMapper mapper)
        {
            _customerRepository = customerRepository;
            _chargingSessionRepository = chargingSessionRepository;
            _mapper = mapper;
        }

        // CRUD Operations
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllAsync();
            var customerDtos = _mapper.Map<List<CustomerResponseDto>>(customers);

            return Ok(customerDtos);
        }
        //Module 44
        [HttpGet("Module44")]
        public  async Task<IActionResult> GetAllPageCustomers([FromQuery] CustomerQueryDto query)
        {
            var result = await _customerRepository.GetCustomersAsync(query);
            var customerDtos = result.customers
                .Select(c=>new CustomerResponseDto
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    Email = c.Email,
                    City = c.City,
                    IsActive = c.IsActive
                })
                .ToList();
            var totalPages = (int)Math.Ceiling(result.TotalRecords/(double)query.PageSize);
            var response = new PageResponseDto<CustomerResponseDto>
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalRecords = result.TotalRecords,
                TotalPages = totalPages,
                Data = customerDtos
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                return NotFound(new { message = "Customer not found" });
            var customerDto = _mapper.Map<CustomerResponseDto>(customer);

            return Ok(customerDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto createCustomerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var customer = _mapper.Map<Customer>(createCustomerDto);

            await _customerRepository.AddAsync(customer);
            var customerDto = _mapper.Map<CustomerResponseDto>(customer);

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customerDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerDto updateCustomerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                return NotFound(new { message = "Customer not found" });
            _mapper.Map(updateCustomerDto,customer);

            _customerRepository.Update(customer);
            await _customerRepository.SaveAsync();

            var customerDto = _mapper.Map<CustomerResponseDto>(customer);

            return Ok(customerDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                return NotFound(new { message = "Customer not found" });

            _customerRepository.Delete(customer);
            await _customerRepository.SaveAsync();

            return NoContent();
        }

        // Advanced queries
        [HttpGet("{id}/vehicles")]
        public async Task<IActionResult> GetCustomerWithVehicles(int id)
        {
            var customer = await _customerRepository.GetCustomerWithVehiclesAsync(id);
            if (customer == null)
                return NotFound(new { message = "Customer not found" });

            var vehicleDtos = customer.Vehicles?.Select(v => new VehicleResponseDto
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Brand = v.Brand,
                Model = v.Model,
                BatteryCapacityKWh = v.BatteryCapacityKWh,
                CustomerId = v.CustomerId
            }).ToList() ?? new List<VehicleResponseDto>();

            var customerDto = new CustomerWithVehiclesDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Email = customer.Email,
                City = customer.City,
                IsActive = customer.IsActive,
                Vehicles = vehicleDtos
            };

            return Ok(customerDto);
        }

        [HttpGet("active/list")]
        public async Task<IActionResult> GetActiveCustomers()
        {
            var customers = await _customerRepository.GetActiveCustomersAsync();
            var customerDtos = customers.Select(c => new CustomerResponseDto
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email,
                City = c.City,
                IsActive = c.IsActive
            }).ToList();

            return Ok(customerDtos);
        }

        [HttpGet("selected-cities")]
        public async Task<IActionResult> GetCustomersFromSelectedCities()
        {
            var customers = await _customerRepository.GetCustomersFromSelectedCitiesRepo();
            var customerDtos = customers.Select(c => new CustomerResponseDto
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email,
                City = c.City,
                IsActive = c.IsActive
            }).ToList();

            return Ok(customerDtos);
        }

        [HttpGet("tesla-exists")]
        public async Task<IActionResult> TeslaExists()
        {
            var exists = await _customerRepository.AreAllStationPresents();

            return Ok(new
            {
                teslaExists = exists
            });
        }

        [HttpGet("statistics/total-vehicles")]
        public async Task<IActionResult> GetVehicleCount()
        {
            var customers = await _customerRepository.GetAllAsync();
            var totalVehicles = customers.Sum(c => c.Vehicles?.Count ?? 0);

            return Ok(new
            {
                totalVehicles = totalVehicles
            });
        }

        [HttpGet("statistics/high-capacity-vehicles")]
        public async Task<IActionResult> GetHighCapacityVehicleCount()
        {
            var customers = await _customerRepository.GetAllAsync();
            var highCapacityCount = customers
                .SelectMany(c => c.Vehicles ?? new List<Vehicle>())
                .Count(v => v.BatteryCapacityKWh > 60);

            return Ok(new
            {
                highCapacityVehicles = highCapacityCount
            });
        }

        [HttpGet("statistics/total-energy")]
        public async Task<IActionResult> GetTotalEnergy()
        {
            var totalEnergy = await _chargingSessionRepository.GetTotalEnergyConsumedAsync();

            return Ok(new
            {
                totalEnergy = totalEnergy
            });
        }

        [HttpGet("statistics/average-energy")]
        public async Task<IActionResult> GetAverageEnergy()
        {
            var average = await _chargingSessionRepository.GetAverageEnergyConsumedAsync();

            return Ok(new
            {
                averageEnergy = average
            });
        }

        [HttpGet("statistics/minimum-session-amount")]
        public async Task<IActionResult> GetMinimumSessionAmount()
        {
            var minimum = await _chargingSessionRepository.GetMinimumAmountAsync();

            return Ok(new
            {
                minimumAmount = minimum
            });
        }

        [HttpGet("statistics/maximum-session-amount")]
        public async Task<IActionResult> GetMaximumSessionAmount()
        {
            var maximum = await _chargingSessionRepository.GetMaximumAmountAsync();

            return Ok(new
            {
                maximumAmount = maximum
            });
        }
        [HttpGet("test-exception")]
        public IActionResult TestException()
        {
            throw new Exception("Testing Global Exception Handling");
        }
    }
}
