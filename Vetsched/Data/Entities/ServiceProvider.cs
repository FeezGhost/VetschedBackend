using System.ComponentModel.DataAnnotations.Schema;

namespace Vetsched.Data.Entities
{
    [Table("services_providers")]
    public class ServiceProvider : BaseEntity
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("profile_id")]
        public Guid ProviderId { get; set; }
        [Column("service_id")]
        public Guid ServiceId { get; set; }
        public UserProfile Provider { get; set; }
        public Service Service { get; set; }
    }
}
