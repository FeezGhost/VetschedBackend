using System.ComponentModel.DataAnnotations.Schema;

namespace Vetsched.Data
{
    public class BaseEntity
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("deleted")]
        public bool Deleted { get; set; }
        [Column("created_by")]
        public string CreatedBy { get; set; }
        [Column("created_when")]
        public DateTimeOffset? CreatedWhen { get; set; }
        [Column("modified_by")]
        public string ModifiedBy { get; set; }
        [Column("modified_when")]
        public DateTimeOffset? ModifiedWhen { get; set; }
    }
}
