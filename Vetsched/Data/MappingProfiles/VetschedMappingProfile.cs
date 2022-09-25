using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Vetsched.Data.Dtos.Request;
using Vetsched.Data.Dtos.Response;
using Vetsched.Data.Entities;
using Vetsched.Helper.Conversions;

namespace Vetsched.Data.MappingProfiles
{
    public class VetschedMappingProfile : Profile
    {
        public VetschedMappingProfile()
        {
            CreateMap<ServicesDto, Service>();
            CreateMap<ServicesDto, Service>().ReverseMap();

            CreateMap<UserBaseResponseDto, ApplicationUser>();
            CreateMap<UserBaseResponseDto, ApplicationUser>().ReverseMap();

            CreateMap<ProviderResponseDto, ApplicationUser>();
            CreateMap<ProviderResponseDto, ApplicationUser>().ReverseMap();

            CreateMap<PetLoverResponseDto, ApplicationUser>();
            CreateMap<PetLoverResponseDto, ApplicationUser>().ReverseMap();

            CreateMap<PetCreateRequestDto, Pet>()
                .ForMember(dto => dto.Allergies, e => e.MapFrom(o => ConvertJObjectToJsonElement((JObject)o.Allergies)))
                .ForMember(dto => dto.Details, e => e.MapFrom(o => ConvertJObjectToJsonElement((JObject)o.Details)))
                .ForMember(dto => dto.Medications, e => e.MapFrom(o => ConvertJObjectToJsonElement((JObject)o.Medications)));

            CreateMap<PetCreateRequestDto, Pet>().ReverseMap();

            CreateMap<PetUpdateResponseDto, Pet>();
            CreateMap<PetUpdateResponseDto, Pet>().ReverseMap()
                .ForMember(dto => dto.Allergies, e => e.MapFrom(o => ConvertJsonElementToJObject((JsonElement)o.Allergies)))
                .ForMember(dto => dto.Details, e => e.MapFrom(o => ConvertJsonElementToJObject((JsonElement)o.Details)))
                .ForMember(dto => dto.Medications, e => e.MapFrom(o => ConvertJsonElementToJObject((JsonElement)o.Medications)));

            CreateMap<PetCreateResponseDto, Pet>();
            CreateMap<PetCreateResponseDto, Pet>().ReverseMap()
                .ForMember(dto => dto.Allergies, e => e.MapFrom(o => ConvertJsonElementToJObject((JsonElement)o.Allergies)))
                .ForMember(dto => dto.Details, e => e.MapFrom(o => ConvertJsonElementToJObject((JsonElement)o.Details)))
                .ForMember(dto => dto.Medications, e => e.MapFrom(o => ConvertJsonElementToJObject((JsonElement)o.Medications)));

            CreateMap<PetResponseDto, Pet>();
            CreateMap<PetResponseDto, Pet>().ReverseMap()
                .ForMember(dto => dto.Allergies, e => e.MapFrom(o => ConvertJsonElementToJObject((JsonElement)o.Allergies)))
                .ForMember(dto => dto.Details, e => e.MapFrom(o => ConvertJsonElementToJObject((JsonElement)o.Details)))
                .ForMember(dto => dto.Medications, e => e.MapFrom(o => ConvertJsonElementToJObject((JsonElement)o.Medications)));
        }

        public JsonElement ConvertJObjectToJsonElement(JObject jObject)
        {
            string jsonString = JsonConvert.SerializeObject(jObject);
            JsonDocument json = JsonDocument.Parse(jsonString);
            return json.RootElement;
        }

        public JObject ConvertJsonElementToJObject(JsonElement jsonElement)
        {
            try
            {
                JObject json = JObject.Parse(jsonElement.ToString());
                return json;
            }
            catch (Exception ex)
            {
                return new JObject();
            }
        }
    }
}
