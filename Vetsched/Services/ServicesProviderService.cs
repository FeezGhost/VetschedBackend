using Loader.infrastructure.GenericRepository;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _repository;
        public ServicesProviderService(
            IRepository<Service, VetschedContext> repositoryService,
            UserManager<ApplicationUser> repository
            )
        {
            _repositoryService = repositoryService;
            _repository = repository;
        }

        public Task<bool> AddServiceToProfile(AddServicesDto request)
        {
            throw new NotImplementedException();
        }

        public Task<List<ServicesDto>> GetAllServices()
        {
            throw new NotImplementedException();
        }

        public Task<List<ServicesDto>> GetProviderServices(Guid ProviderId)
        {
            throw new NotImplementedException();
        }

        public Task<ServicesDto> GetService(Guid ServiceId)
        {
            throw new NotImplementedException();
        }

        public Task<ServicesDto> GetServices(Guid ServiceId)
        {
            throw new NotImplementedException();
        }

        public Task<ServicesDto> GetServicesProvider(Guid ServiceId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveServiceFromProfile(Guid ServiceId, Guid ProfileId)
        {
            throw new NotImplementedException();
        }
    }
}
