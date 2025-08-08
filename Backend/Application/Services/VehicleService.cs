using Application.Dtos.Vehicle;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    public interface IVehicleService
    {
        Task CreateAsync(CreateVehicleDto createDto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Vehicle>> GetAllAsync(Guid workshopId);
        Task<Vehicle> GetByIdAsync(Guid id);
        Task<Vehicle> GetByPlateAsync(string plate, Guid workshopId);
        Task UpdateAsync(Guid Id, UpdateVehicleDto vehicleDto);
    }

    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public VehicleService(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateVehicleDto createDto)
        {
            Vehicle vehicle = _mapper.Map<Vehicle>(createDto);

            await _vehicleRepository.CreateAsync(vehicle);
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync(Guid workshopId)
        {
            return await _vehicleRepository.GetAllAsync(workshopId);
        }

        public async Task<Vehicle> GetByIdAsync(Guid id)
        {
            Vehicle vehicle =
                await _vehicleRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException(id);
            return vehicle;
        }

        public async Task<Vehicle> GetByPlateAsync(string plate, Guid workshopId)
        {
            Vehicle vehicle =
                await _vehicleRepository.GetByPlateAsync(plate, workshopId)
                ?? throw new EntityNotFoundException(plate);
            return vehicle;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Guid Id, UpdateVehicleDto vehicleDto)
        {
            Vehicle vehicle = await GetByIdAsync(Id);

            vehicle.Update(
                plate: vehicleDto.Plate,
                color: vehicleDto.Color,
                model: vehicleDto.Model
            );

            await _vehicleRepository.UpdateAsync(vehicle);
        }
    }
}
