using Vetsched.Data.Enums;

namespace Vetsched.Data.Dtos.Request
{
    public class UserProfileRequestDto
    {
    }
    public class UserProfileCreateRequestDto
    {
        public ProfileType ProfileType { get; set; }
        public int? NumberOfPet { get; set; }
        public Guid UserId { get; set; }
    }
    public class UserProfileUpdateRequestDto
    {
        public Guid Id { get; set; }
        public int? NumberOfPet { get; set; }
    }
}
