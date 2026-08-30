using AutoMapper;
using EVChargingManagementAPI.Controllers;
using EVChargingManagementAPI.DTOs;
using EVChargingManagementAPI.Models;
using EVChargingManagementAPI.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace Ev_VChargingManagementAPI.Tests
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IChargingSessionRepository> _chargingSessionRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IMemoryCache> _cacheMock;
        private readonly Mock<ILogger<CustomersController>> _loggerMock;

        private readonly CustomersController _controller;

        public CustomerControllerTests()
        {
            _customerRepositoryMock =
                new Mock<ICustomerRepository>();

            _chargingSessionRepositoryMock =
                new Mock<IChargingSessionRepository>();

            _mapperMock =
                new Mock<IMapper>();

            _cacheMock =
                new Mock<IMemoryCache>();

            _loggerMock =
                new Mock<ILogger<CustomersController>>();

            _controller = new CustomersController(
                _customerRepositoryMock.Object,
                _chargingSessionRepositoryMock.Object,
                _mapperMock.Object,
                _cacheMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task GetCustomerById_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange

            int customerId = 999;

            _customerRepositoryMock
                .Setup(x => x.GetByIdAsync(customerId))
                .ReturnsAsync((Customer?)null);


            // Act

            var result =
                await _controller.GetCustomerById(customerId);


            // Assert

            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundObjectResult>(result);
        }
        [Fact]
        public async Task CreateCustomer_ReturnsCreated_WhenValidData()
        {
            // Arrange

            var createDto = new CreateCustomerDto
            {
                FullName = "Amit Patil",
                Email = "amit@gmail.com",
                City = "Mumbai",
                IsActive = true
            };

            var customer = new Customer
            {
                Id = 10,
                FullName = "Amit Patil",
                Email = "amit@gmail.com",
                City = "Mumbai",
                IsActive = true
            };

            var responseDto = new CustomerResponseDto
            {
                Id = 10,
                FullName = "Amit Patil",
                Email = "amit@gmail.com",
                City = "Mumbai",
                IsActive = true
            };

            _mapperMock
                .Setup(x => x.Map<Customer>(createDto))
                .Returns(customer);

            _mapperMock
                .Setup(x => x.Map<CustomerResponseDto>(customer))
                .Returns(responseDto);

            _customerRepositoryMock
                .Setup(x => x.AddAsync(customer))
                .Returns(Task.CompletedTask);


            // Act

            var result =
                await _controller.CreateCustomer(createDto);


            // Assert

            var createdResult =
                Assert.IsType<Microsoft.AspNetCore.Mvc.CreatedAtActionResult>(result);

            var response =
                Assert.IsType<CustomerResponseDto>(createdResult.Value);

            Assert.Equal(10, response.Id);
            Assert.Equal("Amit Patil", response.FullName);
            Assert.Equal("amit@gmail.com", response.Email);
        }
    }
}