using AutoMapper;
using Loader.infrastructure.GenericRepository;
using Vetsched.Data.DBContexts;
using Vetsched.Data.Dtos.Request;
using Vetsched.Data.Dtos.Response;
using Vetsched.Data.Entities;
using ServiceProvider = Vetsched.Data.Entities.ServiceProvider;

namespace Vetsched.Services.ServicesProviderSrv
{
    public class ServicesProviderService : IServicesProviderService
    {
        private readonly IRepository<Service, VetschedContext> _repositoryService;
        private readonly IMapper _mapper;
        private readonly IRepository<ServiceProvider, VetschedContext> _repositoryServiceProvider;
        public ServicesProviderService(
            IRepository<Service, VetschedContext> repositoryService,
            IMapper mapper,
            IRepository<ServiceProvider, VetschedContext> repositoryServiceProvider
            )
        {
            _repositoryService = repositoryService;
            _mapper = mapper;
            _repositoryServiceProvider = repositoryServiceProvider;
        }

        public async Task<bool> AddServiceToProfile(AddServicesDto request)
        {
            var services = await _repositoryService.GetMany(x => request.ServiceIds.Contains(x.Id));
            foreach(var service in services)
            {
                var serviceProvider = new ServiceProvider
                {
                    ProviderId = request.ProfileId,
                    ServiceId = service.Id,
                };
                await _repositoryServiceProvider.AddAsync(serviceProvider);
            }
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
            var providers = _repositoryServiceProvider.GetWithInclude(x => x.ProviderId == ProviderId, "Service").ToList();
            var services = providers.Select(x => x.Service).ToList();
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
            var service =  _repositoryServiceProvider.GetWithInclude(x => x.ServiceId == ServiceId, "Provider", "Provider.User").ToList();
            var providers = service.Select(x => x.Provider.User).ToList();
            var providersResponse = _mapper.Map<List<UserBaseResponseDto>>(providers);
            return providersResponse;
        }

        public async Task<bool> RemoveServiceFromProfile(Guid ServiceId, Guid ProfileId)
        {
            var serviceProvider = await _repositoryServiceProvider.GetFirst(x => x.ProviderId == ProfileId && x.ServiceId == ServiceId);
            await _repositoryServiceProvider.DeleteAsync(serviceProvider.Id);
            return true;
        }
    }
}
