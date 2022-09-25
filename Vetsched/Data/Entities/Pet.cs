using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Vetsched.Data.Enums;

namespace Vetsched.Data.Entities
{
    [Table("pets")]
    public class Pet : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("title")]
        public PetTittle Title { get; set; }
        [Column("breed")]
        public string Breed { get; set; }
        [Column("age")]
        public byte Age { get; set; }
        [Column("sex")]
        public Gender Sex { get; set; }
        [Column("microchiped")]
        public bool Microchiped { get; set; }
        [Column("Vaccination")]
        public bool Vaccination { get; set; }
        [Column("sepcies")]
        public string Sepcies { get; set; }
        [Column("allergies")]
        public JsonElement? Allergies { get; set; }
        [Column("medications")]
        public JsonElement? Medications { get; set; } // Add Current = true To check if it's current or not
        [Column("last_vist_description")]
        public string? LastVistDescription { get; set; }
        [Column("vaccine_recieved")]
        public string? VaccineRecieved { get; set; }
        [Column("due_vaccine")]
        public string? DueVaccine { get; set; }
        [Column("Details")]
        public JsonElement? Details { get; set; }
        [Column("pet_lover_id")]
        public Guid PetLoverId { get; set; }


        //TODO :: Add Attachments
        //TODO :: Add Comments
        public UserProfile PetLover { get; set; }
    }
}
