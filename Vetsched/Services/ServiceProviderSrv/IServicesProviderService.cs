using Vetsched.Data.Dtos.Request;
using Vetsched.Data.Dtos.Response;

namespace Vetsched.Services.ServicesProviderSrv
{
    public interface IServicesProviderService
    {
        Task<List<ServicesDto>> GetAllServices();
        Task<ServicesDto> GetService(Guid ServiceId);
        List<ServicesDto> GetProviderServices(Guid ProviderId);
        List<UserBaseResponseDto> GetServicesProvider(Guid ServiceId);
        Task<bool> AddServiceToProfile(AddServicesDto request);
        Task<bool> RemoveServiceFromProfile(Guid ServiceId, Guid ProfileId);
    }
}
