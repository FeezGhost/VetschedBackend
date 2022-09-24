using Vetsched.Data.Entities;
using Vetsched.Data.Enums;

namespace Vetsched.Data.Dtos.Response
{
    public class UserBaseResponseDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public ProfileType ProfileType { get; set; }
        public string ProfileImage { get; set; }
        public bool Status { get; set; }
    }

    public class ProviderResponseDto : UserBaseResponseDto
    {
        public Guid ProfileId { get; set; }
        public bool IsActive { get; set; } 
        public List<Service>? Services { get; set; }
    }
    public class PetLoverResponseDto : UserBaseResponseDto
    {
        public Guid ProfileId { get; set; }
        public bool IsActive { get; set; } 
        public int NumberOfPet { get; set; }
    }
}
