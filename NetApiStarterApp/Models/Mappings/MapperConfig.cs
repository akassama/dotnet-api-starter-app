using AutoMapper;
using NetApiStarterApp.Models.Vehicle;

namespace NetApiStarterApp.Models.Mappings
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            //Vehicle Mapping
            CreateMap<VehicleModel,
                VehicleAddDto>().ReverseMap();
            CreateMap<VehicleModel,
                VehicleUpdateDto>().ReverseMap();
        }
    }
}
