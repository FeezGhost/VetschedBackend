using System.ComponentModel.DataAnnotations.Schema;

namespace Vetsched.Data.Entities
{
    [Table("pets")]
    public class Pet : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        public UserProfile PetLover { get; set; }
    }
}
