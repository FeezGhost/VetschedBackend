using Microsoft.AspNetCore.Identity;

namespace Vetsched.Data.Entities
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public Guid Id { get; set; }
        public bool Deleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedWhen { get; set; }
        public string ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedWhen { get; set; }
    }
}
