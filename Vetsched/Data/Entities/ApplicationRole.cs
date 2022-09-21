using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Vetsched.Data.Enums;

namespace Vetsched.Data.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        [Column("role_identifier")]
        public RoleIdentifier Identifier { get; set; }

        public bool Deleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedWhen { get; set; }
        public string ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedWhen { get; set; }

    }
}
