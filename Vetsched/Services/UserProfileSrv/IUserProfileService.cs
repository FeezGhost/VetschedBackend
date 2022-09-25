using Vetsched.Data.Dtos.Request;
using Vetsched.Data.Dtos.Response;

namespace Vetsched.Services.UserProfileSrv
{
    public interface IUserProfileService
    {
        Task<List<UserBaseResponseDto>> GetUserProfiles(Guid UserId);
        Task<UserBaseResponseDto> GetUserProfile(Guid ProfileId);
        Task<UserBaseResponseDto> CreateUserProfile(UserProfileCreateRequestDto request);
        Task<bool> DeleteUserProfile(Guid ProfileId);
        Task<bool> UpdateUserProfile(UserProfileUpdateRequestDto request);
    }
}
