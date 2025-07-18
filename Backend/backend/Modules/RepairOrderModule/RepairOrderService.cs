using AutoMapper;
using backend.Common.Exceptions;
using backend.Database;
using backend.Database.Entites;
using backend.Database.Repository;
using backend.Modules.CustomerModule.Dtos;
using backend.Modules.RepairOrderModule.Dtos;
using backend.Modules.RepairOrderModule.Interfaces;
using backend.Modules.VehicleModule.Dtos;
using Microsoft.EntityFrameworkCore;

namespace backend.Modules.RepairOrderModule
{
    public class RepairOrderService : Repository<RepairOrder>, IRepairOrderService
    {
        public RepairOrderService(AppDbContext database, IMapper mapper)
            : base(database, mapper) { }

        public async Task AddRepairOrder(CreateRepairOrderWithVehicleDto dto)
        {

            await IsPlateRegisteredInWorkshopAsync(dto.VehicleDto.Plate, Guid.Parse(dto.WorkshopId));
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Guid vehicleId = Guid.NewGuid();

                Vehicle vehicle = new Vehicle()
                {
                    Id = vehicleId,
                    CustomerId = Guid.Parse(dto.VehicleDto.CustomerId),
                    Plate = dto.VehicleDto.Plate,
                    Color = dto.VehicleDto.Color,
                    Model = dto.VehicleDto.Model
                };
                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();

                RepairOrder repairOrder = new RepairOrder()
                {
                    Id = Guid.NewGuid(),
                    WorkShopId = new Guid(dto.WorkshopId),
                    Cause = dto.Cause,
                    Details = dto.Details ?? "",
                    VehicleId = vehicleId,
                    ReceptionDate = dto.ReceptionDate,
                };

                _dbSet.Add(repairOrder);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task AddRepairOrder(CreateRepairOrderWithVehicleAndCustomerDto dto)
        {
            await IsPlateRegisteredInWorkshopAsync(dto.VehicleDto.Plate, Guid.Parse(dto.WorkshopId));

            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Guid customerId = Guid.NewGuid();
                Guid vehicleId = Guid.NewGuid();

                Customer customer = new Customer()
                {
                    FirstName = dto.CustomerDto.FirstName,
                    LastName = dto.CustomerDto.LastName,
                    Id = customerId,
                    WorkShopId = Guid.Parse(dto.WorkshopId),
                    PhoneNumber = dto.CustomerDto.PhoneNumber,
                    Email = dto.CustomerDto.Email,
                    Address = dto.CustomerDto.Address,
                    Dni = dto.CustomerDto.Dni,
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                Vehicle vehicle = new Vehicle()
                {
                    Id = vehicleId,
                    CustomerId = customerId,
                    Plate = dto.VehicleDto.Plate,
                    Color = dto.VehicleDto.Color,
                    Model = dto.VehicleDto.Model
                };

                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();

                RepairOrder repairOrder = new RepairOrder()
                {
                    Id = Guid.NewGuid(),
                    WorkShopId = new Guid(dto.WorkshopId),
                    Cause = dto.Cause,
                    Details = dto.Details ?? "",
                    VehicleId = vehicleId,
                    ReceptionDate = dto.ReceptionDate,
                };

                _dbSet.Add(repairOrder);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<RepairOrder> CreateAsync(CreateRepairOrderDto createDto)
        {
            return await AddWithDto(createDto);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ListRepairOrderDto>> GetAllAsync(Guid workShopId)
        {
            IQueryable<ListRepairOrderDto> listJobs = _dbSet
                .Where(k => k.WorkShopId == workShopId)
                .Include(k => k.SpareParts)
                .Include(k => k.Vehicle)
                .ThenInclude(k => k.Customer)
                .Select(k => new ListRepairOrderDto()
                {
                    Id = k.Id,
                    Cause = k.Cause,
                    Details = k.Details,
                    NotificationSent = k.NotifycationSent,
                    Budget = k.Budget,
                    ReceptionDate = k.ReceptionDate,
                    DeliveryDate = k.DeliveryDate,
                    Status = k.Status.ToString(),
                    SpareParts = k
                        .SpareParts.Select(sp => new SparePart()
                        {
                            Name = sp.Name,
                            Price = sp.Price,
                            Quantity = sp.Quantity,
                        })
                        .ToList(),
                    FinalAmount = k.FinalAmount,
                    Vehicle = new ListJobsVehicleDto()
                    {
                        Id = k.Vehicle.Id,
                        Plate = k.Vehicle.Plate,
                        Model = k.Vehicle.Model,
                    },
                    Client = new ListJobsClientDto()
                    {
                        Id = k.Vehicle.Customer.Id,
                        FirstName = k.Vehicle.Customer.FirstName,
                        LastName = k.Vehicle.Customer.LastName,
                        PhoneNumber = k.Vehicle.Customer.PhoneNumber,
                        Email = k.Vehicle.Customer.Email,
                    },
                })
                .OrderByDescending(repairOrder => repairOrder.ReceptionDate);

            return await listJobs.ToListAsync();
        }

        public async Task<RepairOrder> GetByIdAsync(Guid id)
        {
            var response = await GetById(id) ?? throw new NotFoundException("Vehicle not found");

            return response;
        }

        public async Task UpdateAsync(UpdateRepairOrderDto jobDto)
        {
            var entity =
                await GetByIdAsync(Guid.Parse(jobDto.Id))
                ?? throw new NotFoundException("Job not Found");

            entity.ReceptionDate = jobDto.ReceptionDate;
            entity.DeliveryDate = jobDto.DeliveryDate;
            entity.NotifycationSent = jobDto.NotificationSent;
            entity.Cause = jobDto.Cause;
            entity.Details = jobDto.Details ?? "";
            entity.Budget = jobDto.Budget ?? 0;
            entity.FinalAmount = jobDto.FinalAmount;
            await Update(entity);
        }

        public Task UpdateAsync(RepairOrder entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateSpareParts(List<UpdateSparePartDto> dto, Guid repairOrderId)
        {
            RepairOrder repairOrder = await _dbSet.Where(k => k.Id == repairOrderId).FirstOrDefaultAsync() ??
                                            throw new NotFoundException("Orden de trabajo no encontrada");

            if (_mapper == null) throw new Exception();

            repairOrder.SpareParts = _mapper.Map<List<SparePart>>(dto);

            await Update(repairOrder);
        }

        public async Task UpdateStatusAndFinalAmount(UpdateAmountAndStatusDto dto)
        {
            var entity =
                await GetByIdAsync(Guid.Parse(dto.Id))
                ?? throw new NotFoundException("Job not Found");

            if (Enum.TryParse<RepairOrderStatus>(dto.Status, out var status))
            {
                entity.Status = status;
                entity.FinalAmount = dto.FinalAmount;

                await Update(entity);
            }
            else
            {
                throw new BadHttpRequestException("Invalid status provided");
            }
        }

        Task<IEnumerable<RepairOrder>> IServiceBase<RepairOrder, CreateRepairOrderDto>.GetAllAsync(
            Guid workshopId
        )
        {
            throw new NotImplementedException();
        }

        private async Task IsPlateRegisteredInWorkshopAsync(string plate, Guid workshopId)
        {
            Vehicle? vehicle = await _context
                                        .Vehicles
                                        .Where(vehicle => vehicle.Plate == plate && vehicle.Customer.WorkShopId == workshopId)
                                        .FirstOrDefaultAsync();

            if (vehicle != null)
            {
                throw new ConflictException("La patente ya se encuentra registrada en el taller.");
            }
        }

    }
}
