using AutoMapper;
using Loader.infrastructure.GenericRepository;
using Vetsched.Data.DBContexts;
using Vetsched.Data.Dtos.Request;
using Vetsched.Data.Dtos.Response;
using Vetsched.Data.Entities;

namespace Vetsched.Services.UserProfileSrv
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IRepository<UserProfile, VetschedContext> _repository;
        private readonly IMapper _mapper;
        public UserProfileService(
            IRepository<UserProfile, VetschedContext> repository,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<UserBaseResponseDto> CreateUserProfile(UserProfileCreateRequestDto request)
        {
            UserProfile profile = new UserProfile
            {
                UserId = request.UserId,
                ProfileType = request.ProfileType,
                IsActive = true,
            };
            if(request.NumberOfPet is not null)
            {
                profile.NumberOfPet = (int)request.NumberOfPet;
            }
            var createdProfile = await _repository.AddAsync(profile);
            var response = _mapper.Map<UserBaseResponseDto>(createdProfile);
            return response;
        }

        public async Task<bool> DeleteUserProfile(Guid ProfileId)
        {
            var res = await _repository.DeleteAsync(ProfileId);
            return true;
        }

        public async Task<UserBaseResponseDto> GetUserProfile(Guid ProfileId)
        {
            var profile = await _repository.GetByIdAsync(ProfileId);
            var response = _mapper.Map<UserBaseResponseDto>(profile);
            return response;
        }

        public async Task<List<UserBaseResponseDto>> GetUserProfiles(Guid UserId)
        {
            var profiles = await _repository.GetMany(x => x.UserId == UserId);
            var response = _mapper.Map<List<UserBaseResponseDto>>(profiles);
            return response;
        }

        public async Task<bool> UpdateUserProfile(UserProfileUpdateRequestDto request)
        {
            var profile = await _repository.GetByIdAsync(request.Id);
            if(request.NumberOfPet is not null)
            {
                if(request.NumberOfPet != profile.NumberOfPet)
                {
                    profile.NumberOfPet = (int)request.NumberOfPet;
                    await _repository.UpdateAsync(profile);
                }
            }
            return true;
        }
    }
}
