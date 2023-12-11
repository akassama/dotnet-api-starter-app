using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NetApiStarterApp.Data;
using NetApiStarterApp.Models.Vehicle;
using System.Reflection;

namespace NetApiStarterApp.Repository.Vehicle
{
    public class VehicleService : IVehicleService
    {
        private readonly DataConnection _dbContext;
        private IMapper _mapper { get; }

        public VehicleService(DataConnection dataConnection, IMapper mapper)
        {
            _dbContext = dataConnection;
            _mapper = mapper;
        }

        public async Task<List<VehicleModel>> GetVehicleListAsync()
        {
            return await _dbContext.Vehicles.ToListAsync();
        }

        public async Task<VehicleModel> GetVehicleByIdAsync(Guid vehicleId)
        {
            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.VehicleId == vehicleId);

            if (vehicle == null)
            {
                // Handle the case where the vehicle is not found
                throw new InvalidOperationException("Vehicle not found");
            }

            return vehicle;
        }

        public async Task<VehicleModel> AddVehicleAsync(AddVehicleDto vehicleAddDto)
        {
            // Add new vehicle
            var vehicleModel =  _mapper.Map<VehicleModel>(vehicleAddDto);

            vehicleModel.VehicleId = Guid.NewGuid();
            vehicleModel.UploadedBy = Guid.NewGuid();//TODO
            vehicleModel.CreatedAt = DateTime.UtcNow;
            await _dbContext.Vehicles.AddAsync(vehicleModel);

            await _dbContext.SaveChangesAsync();
            return vehicleModel;
        }

        public async Task<VehicleModel> UpdateVehicleAsync(UpdateVehicleDto vehicleUpdateDto)
        {

            // Update existing vehicle
            var vehicleModel = _mapper.Map<VehicleModel>(vehicleUpdateDto);

            var existingVehicle = await _dbContext.Vehicles
                .FirstOrDefaultAsync(v => v.VehicleId == vehicleUpdateDto.VehicleId);

            if (existingVehicle != null)
            {
                existingVehicle.Make = vehicleModel.Make;
                existingVehicle.Model = vehicleModel.Model;
                existingVehicle.Year = vehicleModel.Year;
                existingVehicle.Color = vehicleModel.Color;
                existingVehicle.Mileage = vehicleModel.Mileage;
                existingVehicle.Price = vehicleModel.Price;
                existingVehicle.ImagePath = vehicleModel.ImagePath;
                existingVehicle.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                existingVehicle = new VehicleModel();
            }

            await _dbContext.SaveChangesAsync();
            return existingVehicle;
        }

        public async Task<bool> DeleteVehicleAsync(Guid vehicleId)
        {
            var vehicleToDelete = await _dbContext.Vehicles
                .FirstOrDefaultAsync(v => v.VehicleId == vehicleId);

            if (vehicleToDelete != null)
            {
                _dbContext.Vehicles.Remove(vehicleToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<string?> GetVehicleData(Guid vehicleId, string returnColumn)
        {
            var vehicle = await _dbContext.Vehicles
                .FirstOrDefaultAsync(v => v.VehicleId == vehicleId);

            if (vehicle == null)
            {
                return null;
            }

            var propertyInfo = typeof(VehicleModel).GetProperty(returnColumn, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo != null)
            {
                var value = propertyInfo.GetValue(vehicle);
                return value?.ToString();
            }

            return null;
        }
    }
}
