using AutoMapper;
using NetApiStarterApp.Models.Account;
using NetApiStarterApp.Models.Vehicle;

namespace NetApiStarterApp.Models.Mappings
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            //Vehicle Mapping
            CreateMap<VehicleModel,
                AddVehicleDto>().ReverseMap();
            CreateMap<VehicleModel,
                UpdateVehicleDto>().ReverseMap();

            //Account Mapping
            CreateMap<AccountModel,
                AddAccountDto>().ReverseMap();
            CreateMap<AccountModel,
                UpdateAccountDto>().ReverseMap();
            CreateMap<AccountDetailsModel,
                UpdateAccountDetailsDto>().ReverseMap();
        }
    }
}
