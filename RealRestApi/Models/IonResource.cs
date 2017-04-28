using Newtonsoft.Json;

namespace RealRestApi.Models
{
    public abstract class IonResource
    {
        [JsonProperty(Order = -2)]
        public IonLink Meta { get; set; }
    }
}
