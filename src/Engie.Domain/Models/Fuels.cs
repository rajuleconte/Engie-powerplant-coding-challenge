using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Engie.Domain.Models
{
    public class Fuels
    {
        [JsonProperty(PropertyName = "gas(euro/MWh)")]
        public double Gas { get; set; }
        [JsonProperty(PropertyName = "kerosine(euro/MWh)")]
        public double Kerosine { get; set; }
        [JsonProperty(PropertyName = "co2(euro/ton)")]
        public double Co2 { get; set; }
        [JsonProperty(PropertyName = "wind(%)")]
        public double Wind { get; set; }
    }
}
