using AutoMapper;
using Loader.infrastructure.GenericRepository;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Vetsched.Data.DBContexts;
using Vetsched.Data.Dtos.Request;
using Vetsched.Data.Dtos.Response;
using Vetsched.Data.Entities;

namespace Vetsched.Services
{
    public class ServicesProviderService : IServicesProviderService
    {
        private readonly IRepository<Service, VetschedContext> _repositoryService;
        private readonly IRepository<UserProfile, VetschedContext> _repositoryProfile;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _repository;
        public ServicesProviderService(
            IRepository<Service, VetschedContext> repositoryService,
            UserManager<ApplicationUser> repository,
            IMapper mapper,
            IRepository<UserProfile, VetschedContext> repositoryProfile
            )
        {
            _repositoryService = repositoryService;
            _repository = repository;
            _mapper = mapper;
            _repositoryProfile = repositoryProfile;
        }

        public async Task<bool> AddServiceToProfile(AddServicesDto request)
        {
            var services = await _repositoryService.GetMany(x => request.ServiceIds.Contains(x.Id));
            var provider = _repositoryProfile.GetOneDefaultWithInclude(x => x.Id == request.ProfileId, "Services");
            provider.Services.AddRange(services);
            var response = await _repositoryProfile.UpdateAsync(provider);
            return true;
        }

        public async Task<List<ServicesDto>> GetAllServices()
        {
            var services = await _repositoryService.GetAllAsync();
            var servicesResponse = _mapper.Map<List<ServicesDto>>(services);
            return servicesResponse;
        }

        public List<ServicesDto> GetProviderServices(Guid ProviderId)
        {
            var provider = _repositoryProfile.GetOneDefaultWithInclude(x => x.Id == ProviderId, "Services");
            var services = provider.Services;
            var servicesResponse = _mapper.Map<List<ServicesDto>>(services);
            return servicesResponse.ToList();
        }

        public async Task<ServicesDto> GetService(Guid ServiceId)
        {
            var service = await _repositoryService.GetByIdAsync(ServiceId);
            var serviceResponse = _mapper.Map<ServicesDto>(service);
            return serviceResponse;
        }

        public Task<ServicesDto> GetServices(Guid ServiceId)
        {
            throw new NotImplementedException();
        }

        public List<UserBaseResponseDto> GetServicesProvider(Guid ServiceId)
        {
            var service =  _repositoryService.GetOneDefaultWithInclude(x => x.Id == ServiceId, "Providers", "Providers.User");
            var providers = service.Providers.Select(x => x.User).ToList();
            var providersResponse = _mapper.Map<List<UserBaseResponseDto>>(providers);
            return providersResponse;
        }

        public async Task<bool> RemoveServiceFromProfile(Guid ServiceId, Guid ProfileId)
        {
            var provider = _repositoryProfile.GetOneDefaultWithInclude(x => x.Id == ProfileId, "Services");
            provider.Services = provider.Services.Where(x => x.Id != ServiceId).ToList();
            var response = await _repositoryProfile.UpdateAsync(provider);
            return true;
        }
    }
}
