using Application.Dtos.Customer;
using Application.Dtos.Vehicle;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();

            CreateMap<Vehicle, CreateVehicleDto>().ReverseMap();
            CreateMap<Vehicle, UpdateVehicleDto>().ReverseMap();
            CreateMap<Vehicle, HistoricalRepairOrderDto>().ReverseMap();
        }
    }
}
