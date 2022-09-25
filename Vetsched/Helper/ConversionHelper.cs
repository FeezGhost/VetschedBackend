using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Vetsched.Helper
{
    public class ConversionHelper : IConversionHelper
    {
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
