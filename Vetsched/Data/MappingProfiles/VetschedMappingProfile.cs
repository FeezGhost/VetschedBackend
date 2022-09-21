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
        }
    }
}
