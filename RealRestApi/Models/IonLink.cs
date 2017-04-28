using Newtonsoft.Json;

namespace RealRestApi.Models
{
    public class IonLink
    {
        public string Href { get; set; }

        [JsonProperty(PropertyName = "rel", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Relations { get; set; }
    }
}
