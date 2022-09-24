using AutoMapper;
using Vetsched.Data.Dtos.Response;
using Vetsched.Data.Entities;

namespace Vetsched.Data.MappingProfiles
{
    public class VetschedMappingProfile : Profile
    {
        public VetschedMappingProfile()
        {
            CreateMap<ServicesDto, Service>();
            CreateMap<ServicesDto, Service>().ReverseMap();

            CreateMap<UserBaseResponseDto, ApplicationUser>();
            CreateMap<UserBaseResponseDto, ApplicationUser>().ReverseMap();

            CreateMap<ProviderResponseDto, ApplicationUser>();
            CreateMap<ProviderResponseDto, ApplicationUser>().ReverseMap();

            CreateMap<PetLoverResponseDto, ApplicationUser>();
            CreateMap<PetLoverResponseDto, ApplicationUser>().ReverseMap();
        }
    }
}
