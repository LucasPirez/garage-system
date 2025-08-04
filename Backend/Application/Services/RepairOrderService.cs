using System.Data;
using Application.Dtos.Customer;
using Application.Dtos.RepairOrder;
using Application.Dtos.Vehicle;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    public class RepairOrderService
    {
        private readonly IRepairOrderRepository _repairOrderRepository;
        private readonly ICustomerService _customerService;
        private readonly IVehicleService _vehicleService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RepairOrderService(
            IRepairOrderRepository repository,
            ICustomerService customerService,
            IVehicleService vehicleService,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _repairOrderRepository = repository;
            _customerService = customerService;
            _vehicleService = vehicleService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateRepairOrderAsync(CreateRepairOrderWithVehicleDto dto)
        {
            await IsPlateRegisteredInWorkshopAsync(
                dto.VehicleDto.Plate,
                Guid.Parse(dto.WorkshopId)
            );

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _vehicleService.CreateAsync(dto.VehicleDto);

                Vehicle vehicle = await _vehicleService.GetByIdAsync(dto.VehicleDto.Id);

                RepairOrder repairOrder = new RepairOrder(
                    id: Guid.NewGuid(),
                    workshopId: new Guid(dto.WorkshopId),
                    cause: dto.Cause,
                    details: dto.Details ?? "",
                    vehicle: vehicle,
                    recepcionDate: dto.ReceptionDate,
                    spareParts: new List<SparePart>(),
                    deliveriDate: null
                );

                await _repairOrderRepository.CreateAsync(repairOrder);

                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task CreateRepairOrderAsync(CreateRepairOrderWithVehicleAndCustomerDto dto)
        {
            await IsPlateRegisteredInWorkshopAsync(
                dto.VehicleDto.Plate,
                Guid.Parse(dto.WorkshopId)
            );

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                CreateCustomerDto createCustomerDto = _mapper.Map<CreateCustomerDto>(
                    dto.CustomerDto
                );
                createCustomerDto.WorkshopId = dto.WorkshopId;
                await _customerService.CreateAsync(createCustomerDto);

                CreateVehicleDto createVehicleDto = _mapper.Map<CreateVehicleDto>(dto.VehicleDto);
                createVehicleDto.CustomerId = createCustomerDto.Id.ToString();

                await _vehicleService.CreateAsync(createVehicleDto);

                Vehicle vehicle = await _vehicleService.GetByIdAsync(dto.VehicleDto.Id);

                RepairOrder repairOrder = new RepairOrder(
                    id: dto.Id,
                    workshopId: new Guid(dto.WorkshopId),
                    cause: dto.Cause,
                    details: dto.Details ?? "",
                    vehicle: vehicle,
                    recepcionDate: dto.ReceptionDate,
                    spareParts: new List<SparePart>(),
                    deliveriDate: null
                );

                await _repairOrderRepository.CreateAsync(repairOrder);

                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task CreateRepairOrderAsync(CreateRepairOrderDto createDto)
        {
            RepairOrder repairOrder = new RepairOrder(
                id: createDto.Id,
                workshopId: Guid.Parse(createDto.WorkshopId),
                cause: createDto.Cause,
                details: createDto.Details ?? "",
                vehicle: _mapper.Map<Vehicle>(createDto.Vehicle),
                recepcionDate: createDto.ReceptionDate,
                spareParts: new List<SparePart>(),
                deliveriDate: null
            );

            await _repairOrderRepository.CreateAsync(repairOrder);
        }

        public async Task<IEnumerable<ListRepairOrderDto>> GetAllAsync(Guid workShopId)
        {
            IEnumerable<RepairOrder> repairOrders = await _repairOrderRepository.GetAllAsync(
                workShopId
            );

            return repairOrders.Select(repairOrder => new ListRepairOrderDto()
            {
                Id = repairOrder.Id,
                ReceptionDate = repairOrder.ReceptionDate,
                DeliveryDate = repairOrder.DeliveryDate,
                Status = repairOrder.Status.ToString(),
                Cause = repairOrder.Cause,
                Details = repairOrder.Details,
                Budget = repairOrder.Budget,
                FinalAmount = repairOrder.FinalAmount,
                Vehicle = new BaseVehicleDto()
                {
                    Id = repairOrder.Vehicle.Id,
                    Plate = repairOrder.Vehicle.Plate,
                    Model = repairOrder.Vehicle.Model,
                    Color = repairOrder.Vehicle.Color,
                    CustomerId = repairOrder.Vehicle.CustomerId.ToString(),
                },
                Client = new BaseCustomerDto()
                {
                    Id = repairOrder.Customer?.Id ?? Guid.Empty,
                    FirstName = repairOrder.Customer?.FirstName ?? "Not Included",
                    LastName = repairOrder.Customer?.LastName ?? "Not Included",
                    Email = repairOrder?.Customer?.Email.ToList() ?? new List<string>(),
                    PhoneNumber = repairOrder?.Customer?.PhoneNumber.ToList() ?? new List<string>(),
                },
            });
        }

        public async Task<RepairOrder> GetByIdAsync(Guid id)
        {
            RepairOrder repairOrder =
                await _repairOrderRepository.GetByIdAsync(id)
                ?? throw new EntityNotFoundException(id);

            return repairOrder;
        }

        public async Task UpdateAsync(UpdateRepairOrderDto dto)
        {
            RepairOrder repairOrder =
                await GetByIdAsync(Guid.Parse(dto.Id)) ?? throw new EntityNotFoundException(dto.Id);

            repairOrder.Update(
                cause: dto.Cause,
                budget: dto.Budget,
                notifycationSent: dto.NotificationSent,
                details: dto.Details ?? "",
                finalAmount: dto.FinalAmount,
                recepcionDate: dto.ReceptionDate,
                deliveriDate: dto.DeliveryDate
            );

            await _repairOrderRepository.UpdateAsync(repairOrder);
        }

        public async Task UpdateSpareParts(List<UpdateSparePartDto> dto, Guid repairOrderId)
        {
            RepairOrder repairOrder =
                await GetByIdAsync(repairOrderId)
                ?? throw new EntityNotFoundException(repairOrderId);

            List<SparePart> spareParts = dto.Select(_mapper.Map<SparePart>).ToList();
            if (spareParts[0]?.Name == null)
                throw new Exception("is null");
            repairOrder.UpdateSpareParts(spareParts);

            await _repairOrderRepository.UpdateAsync(repairOrder);
        }

        public async Task UpdateStatusAndFinalAmount(UpdateAmountAndStatusDto dto)
        {
            RepairOrder repairOrder =
                await GetByIdAsync(dto.Id) ?? throw new EntityNotFoundException(dto.Id);

            if (Enum.TryParse<RepairOrderStatus>(dto.Status, out var status))
            {
                repairOrder.UpdateStatusAndFinalAmount(status, dto.FinalAmount);

                await _repairOrderRepository.UpdateAsync(repairOrder);
            }
            else
            {
                throw new ConflictException("Invalid status provided");
            }
        }

        private async Task IsPlateRegisteredInWorkshopAsync(string plate, Guid workshopId)
        {
            try
            {
                Vehicle vehicle = await _vehicleService.GetByPlateAsync(plate, workshopId);

                if (vehicle != null)
                {
                    throw new ConflictException(
                        $"The vehicle with plate {plate} is already registered in the workshop."
                    );
                }
            }
            catch (Exception ex)
            {
                if (ex is EntityNotFoundException)
                {
                    return;
                }
                throw;
            }
        }
    }
}
