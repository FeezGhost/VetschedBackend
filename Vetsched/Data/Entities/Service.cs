using System.ComponentModel.DataAnnotations.Schema;
using Vetsched.Data.Enums;

namespace Vetsched.Data.Entities
{
    [Table("services")]
    public class Service : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("category")]
        public ServiceCategory Category { get; set; }
        public List<ServiceProvider> Providers { get; set; }
    }
}
