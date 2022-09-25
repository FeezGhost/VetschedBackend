using Microsoft.AspNetCore.Mvc;
using Vetsched.Data.Dtos.Request;
using Vetsched.Data.Dtos.Response;
using Vetsched.Services.UserProfileSrv;

namespace Vetsched.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _service;
        public UserProfileController(IUserProfileService service)
        {
            _service = service;
        }

        [HttpGet("Profiles")]
        public async Task<ActionResult<List<UserBaseResponseDto>>> GetProfiles(Guid UserId)
        {
            var res = await _service.GetUserProfiles(UserId);
            return Ok(res);
        }
        [HttpGet("Profile")]
        public async Task<ActionResult<UserBaseResponseDto>> GetProfile(Guid ProfileId)
        {
            var res = await _service.GetUserProfile(ProfileId);
            return Ok(res);
        }
        [HttpPost("Add")]
        public async Task<ActionResult<UserBaseResponseDto>> CreateProfile(UserProfileCreateRequestDto request)
        {
            var res = await _service.CreateUserProfile(request);
            return Ok(res);
        }
        [HttpPut("Update")]
        public async Task<ActionResult<UserBaseResponseDto>> UpdateProfile(UserProfileUpdateRequestDto request)
        {
            var res = await _service.UpdateUserProfile(request);
            return Ok(res);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<UserBaseResponseDto>> DeleteProfile(Guid ProfileId)
        {
            var res = await _service.DeleteUserProfile(ProfileId);
            return Ok(res);
        }
    }
}
