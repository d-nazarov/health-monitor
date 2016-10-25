using HealthMonitorServer.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HealthMonitorServer.Helpers
{
    public static class JsonHelper
    {
        public static string GetJsonMessage(MessageTypes messageType, object data)
        {
            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            return JsonConvert.SerializeObject(new
            {
                messageType = messageType.ToString(),
                data
            }, Formatting.Indented, jsonSerializerSettings);
        }
    }
}
