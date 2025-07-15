using AutoMapper;
using backend.Database.Entites;
using backend.Modules.CustomerModule.Dtos;
using backend.Modules.RepairOrderModule.Dtos;
using backend.Modules.VehicleModule.Dtos;

namespace backend.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Vehicle, CreateVehicleDto>().ReverseMap();
            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<RepairOrder, CreateRepairOrderDto>().ReverseMap();
            CreateMap<UpdateSparePartDto, SparePart>();
        }
    }
}
