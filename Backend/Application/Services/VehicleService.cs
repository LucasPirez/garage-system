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
        Task<IEnumerable<VehicleDto>> GetAllAsync(Guid workshopId);
        Task<VehicleDto> GetByIdAsync(Guid id);
        Task<VehicleDto> GetByPlateAsync(string plate, Guid workshopId);
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

        public async Task<IEnumerable<VehicleDto>> GetAllAsync(Guid workshopId)
        {
            IEnumerable<Vehicle> vehicles = await _vehicleRepository.GetAllAsync(workshopId);

            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles.ToList());
        }

        public async Task<VehicleDto> GetByIdAsync(Guid id)
        {
            Vehicle vehicle =
                await _vehicleRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException(id);

            return _mapper.Map<VehicleDto>(vehicle);
            ;
        }

        public async Task<VehicleDto> GetByPlateAsync(string plate, Guid workshopId)
        {
            Vehicle vehicle =
                await _vehicleRepository.GetByPlateAsync(plate, workshopId)
                ?? throw new EntityNotFoundException(plate);

            return _mapper.Map<VehicleDto>(vehicle);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Guid Id, UpdateVehicleDto vehicleDto)
        {
            Vehicle vehicle =
                await _vehicleRepository.GetByIdAsync(Id) ?? throw new EntityNotFoundException(Id);

            vehicle.Update(
                plate: vehicleDto.Plate,
                color: vehicleDto.Color,
                model: vehicleDto.Model
            );

            await _vehicleRepository.UpdateAsync(vehicle);
        }
    }
}
