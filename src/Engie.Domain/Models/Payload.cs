using Newtonsoft.Json;

namespace Engie.Domain.Models
{
    public class Payload
    {
        public int Load { get; set; }
        [JsonProperty(PropertyName = "fuels")]
        public Fuels Fuels { get; set; } = default!;
        [JsonProperty(PropertyName = "powerplants")]
        public Powerplant[] Powerplants { get; set; } = default!;
    }
}
