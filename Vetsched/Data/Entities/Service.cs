using System.ComponentModel.DataAnnotations.Schema;

namespace Vetsched.Data.Entities
{
    [Table("services")]
    public class Service : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        public List<UserProfile> Providers { get; set; }
    }
}
