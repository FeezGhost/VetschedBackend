using Vetsched.Data.Enums;

namespace Loader.identity_micro_service.Core.Dto
{
    public class SignUpRequestDto
    {
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ProfileType ProfileType { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public string? Country { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
    }
}
