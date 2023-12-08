using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetApiStarterApp.Models.Vehicle;
using NetApiStarterApp.Repository.Vehicle;

namespace NetApiStarterApp.Controllers.Vehicle
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService; 
        public VehiclesController(IVehicleService vehicleService) 
        {
            _vehicleService = vehicleService;
        }

        [AllowAnonymous]
        [HttpGet("get-vehicles")]
        public async Task<IActionResult> GetVehicles()
        {
            var data = await _vehicleService.GetVehicleListAsync();

            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("get-vehicle")]
        public async Task<IActionResult> GetVehicle(Guid vehicleId)
        {
            var data = await _vehicleService.GetVehicleByIdAsync(vehicleId);

            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPost("add-vehicle")]
        public async Task<IActionResult> AddVehicle(AddVehicleDto vehicleRequest)
        {
            var data = await _vehicleService.AddVehicleAsync(vehicleRequest);

            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPost("update-vehicle")]
        public async Task<IActionResult> UpdateVehicle(UpdateVehicleDto vehicleRequest)
        {
            var data = await _vehicleService.UpdateVehicleAsync(vehicleRequest);

            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPost("remove-vehicle")]
        public async Task<IActionResult> RemoveVehicle(Guid vehicleId)
        {
            var data = await _vehicleService.DeleteVehicleAsync(vehicleId);

            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPost("get-vehicle-data")]
        public async Task<IActionResult> GetVehicleData(Guid vehicleId, string returnColumn)
        {
            var data = await _vehicleService.GetVehicleData(vehicleId, returnColumn);

            return Ok(data);
        }
    }
}
