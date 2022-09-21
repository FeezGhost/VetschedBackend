using Vetsched.Data.Dtos.Request;
using Vetsched.Data.Dtos.Response;

namespace Vetsched.Services
{
    public interface IServicesProviderService
    {
        Task<List<ServicesDto>> GetAllServices();
        Task<ServicesDto> GetService(Guid ServiceId);
        Task<List<ServicesDto>> GetProviderServices(Guid ProviderId);
        Task<ServicesDto> GetServicesProvider(Guid ServiceId);
        Task<bool> AddServiceToProfile(AddServicesDto request);
        Task<bool> RemoveServiceFromProfile(Guid ServiceId, Guid ProfileId);
    }
}
