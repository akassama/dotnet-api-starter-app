using NetApiStarterApp.Models.Vehicle;

namespace NetApiStarterApp.Repository.Vehicle
{
    public interface IVehicleService
    {
        public Task<List<VehicleModel>> GetVehicleListAsync(); //get list of all vehicle
        public Task<VehicleModel> GetVehicleByIdAsync(Guid vehicleId); //gets vehicle by vehicle id
        public Task<VehicleModel> AddVehicleAsync(VehicleAddDto crudModel); //adds new vehicle data
        public Task<VehicleModel> UpdateVehicleAsync(VehicleUpdateDto crudModel); //update vehicle data
        public Task<bool> DeleteVehicleAsync(Guid vehicleId); //removes vehicle by vehicleId
        public Task<string?> GetVehicleData(Guid vehicleId, string returnColumn); //takes Vehicle as model and a returnColumn as parameters and returns the data.
    }
}
