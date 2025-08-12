using Application.Dtos.Vehicle;
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
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<Customer, EFCustomer>().ReverseMap();
            CreateMap<EFSparePart, SparePart>().ReverseMap();

            CreateMap<RepairOrder, EFRepairOrder>()
                .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.Vehicle.Id))
                .ForMember(dest => dest.Vehicle, opt => opt.Ignore());
            CreateMap<EFRepairOrder, RepairOrder>()
                .ConstructUsing(
                    (src, context) =>
                        new RepairOrder(
                            id: src.Id,
                            recepcionDate: src.ReceptionDate,
                            cause: src.Cause,
                            details: src.Details,
                            workshopId: src.WorkShopId,
                            vehicle: context.Mapper.Map<Vehicle>(src.Vehicle),
                            spareParts: context.Mapper.Map<List<SparePart>>(src.SpareParts),
                            deliveryDate: src.DeliveryDate,
                            customer: context.Mapper.Map<Customer>(src.Vehicle?.Customer),
                            budget: src.Budget,
                            finalAmount: src.FinalAmount,
                            status: (RepairOrderStatus)src.Status,
                            notifycationSent: src.NotifycationSent
                        )
                );

            CreateMap<WorkShop, EFWorkShop>().ReverseMap();
            CreateMap<Admin, EFAdmin>()
                .ForMember(dest => dest.WorkShopId, opt => opt.MapFrom(src => src.WorkShop.Id))
                .ForMember(dest => dest.WorkShop, opt => opt.Ignore());
            CreateMap<EFAdmin, Admin>();
        }
    }
}
