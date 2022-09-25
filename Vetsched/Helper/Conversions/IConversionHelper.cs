using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Vetsched.Helper.Conversions
{
    public interface IConversionHelper
    {
        JsonElement ConvertJObjectToJsonElement(JObject jObject);
        JObject ConvertJsonElementToJObject(JsonElement jsonElement);
    }
}
