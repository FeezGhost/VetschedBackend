using Newtonsoft.Json.Linq;
using System.Text.Json;
using Vetsched.Data.Enums;

namespace Vetsched.Data.Dtos.Request
{
    public class PetRequestDto
    {
    }
    public class PetCreateRequestDto
    {
        public string Name { get; set; }
        public PetTittle Title { get; set; }
        public string Breed { get; set; }
        public byte Age { get; set; }
        public Gender Sex { get; set; }
        public bool Microchiped { get; set; }
        public bool Vaccination { get; set; }
        public string Sepcies { get; set; }
        public JsonElement? Allergies { get; set; }
        public JsonElement? Medications { get; set; } // Add Current = true To check if it's current or not
        public string? LastVistDescription { get; set; }
        public string? VaccineRecieved { get; set; }
        public string? DueVaccine { get; set; }
        public JsonElement? Details { get; set; }
    }
    public class PetUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public PetTittle? Title { get; set; }
        public string? Breed { get; set; }
        public byte? Age { get; set; }
        public Gender? Sex { get; set; }
        public bool? Microchiped { get; set; }
        public bool? Vaccination { get; set; }
        public string? Sepcies { get; set; }
        public JObject? Allergies { get; set; }
        public JObject? Medications { get; set; } // Add Current = true To check if it's current or not
        public string? LastVistDescription { get; set; }
        public string? VaccineRecieved { get; set; }
        public string? DueVaccine { get; set; }
        public JObject? Details { get; set; }
    }
}
