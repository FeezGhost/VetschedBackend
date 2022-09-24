using Vetsched.Data.Enums;

namespace Vetsched.Data.Entities
{
    public class UserProfile : BaseEntity
    {
        public ProfileType ProfileType { get; set; }
        public bool IsActive { get; set; } = true;
        public int NumberOfPet { get; set; }
        public Guid UserId { get; set; }
        public List<Service>? Services { get; set; }
        public List<Pet>? Pets { get; set; }
        public ApplicationUser User { get; set; }
    }
}
