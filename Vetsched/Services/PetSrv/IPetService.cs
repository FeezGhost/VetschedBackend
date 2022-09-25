using Vetsched.Data.Dtos.Request;
using Vetsched.Data.Dtos.Response;

namespace Vetsched.Services.PetSrv
{
    public interface IPetService
    {
        Task<PetResponseDto> GetPet(Guid PetId);
        Task<List<PetResponseDto>> GetPets(Guid PetLoverId);
        Task<List<PetMinimalResponseDto>> GetPetsMinimal(Guid PetLoverId);
        Task<PetCreateResponseDto> CreatePet(PetCreateRequestDto request);
        Task<bool> UpdatePet(PetUpdateRequestDto request);
        Task<bool> DeletePet(Guid PetId);
    }
}
