using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Vetsched.Data.Enums;

namespace Vetsched.Data.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public ProfileType ProfileType { get; set; }
        public ApplicationUser? Manager { get; set; }
        public string VerificationCode { get; set; }
        public string ProfileImage { get; set; }
        public bool Status { get; set; }
        //[ForeignKey("ActiveAccount")]
        //public virtual Account Account { get; set; }
        public bool Deleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedWhen { get; set; }
        public string ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedWhen { get; set; }

        public List<UserProfile>? Profiles { get; set; }

        #region
        #endregion
    }
}
