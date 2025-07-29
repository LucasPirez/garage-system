using AutoMapper;
using Domain.Entities;
using Infraestructure.DataModel;

namespace Infraestructure.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Vehicle, EFVehicle>().ReverseMap();
            CreateMap<Customer, EFCustomer>().ReverseMap();
            CreateMap<RepairOrder, EFRepairOrder>().ReverseMap();
            CreateMap<WorkShop, EFWorkShop>();
            CreateMap<Admin, EFAdmin>();
        }
    }
}
