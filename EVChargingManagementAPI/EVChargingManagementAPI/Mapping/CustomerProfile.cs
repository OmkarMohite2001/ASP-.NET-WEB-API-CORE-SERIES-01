using AutoMapper;
using EVChargingManagementAPI.DTOs;
using EVChargingManagementAPI.Models;

namespace EVChargingManagementAPI.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()

        {
            CreateMap<Customer, CustomerResponseDto>();     //Get
            CreateMap<CreateCustomerDto, Customer>();   //post
            CreateMap<UpdateCustomerDto, Customer>();   //put

        }
    }
}
