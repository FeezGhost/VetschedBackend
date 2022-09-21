using Loader.identity_micro_service.Core.Dto;
using Loader.identity_micro_service.Models;
using Vetsched.Data.Dtos;

namespace Vetsched.Services
{
    public interface IdentityServiceInterface
    {
        public Task<(int, string)> Login(IdentityAuthRequestModel authRequest);
        //public Task<string> OTPVerification(OTPRequestDto oTPRequestDto);
        public Task<GenericDto<string>> Register(SignUpRequestDto registrationRequest);
        //public Task<GenericDto<bool>> CreateUser(SignUpRequestDto newUserDto, Guid accountId, string state, Guid LoggedInUserId, string token = null);
        //public Task CreateOrUpdateRole(RoleRequestDto roleRequestDto);
        //public Task<ListItemsResponseDto> GetRole(Guid roleId);
        public Task<GenericDto<string>> ConfirmUserEmail(string Token, string Email);
        public Task<GenericDto<bool>> ResetPassword(Guid Id, string Password);
        public Task<GenericDto<string>> ForgetPassword(string email);
        //public Task<RequestResponse> SubAccountToken(Guid userId, Guid subAccountId);
        public Task<bool> Logout(Guid userId);
    }
}
