using Engie.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Engie.Domain.Models
{
    public class Powerplant
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; } = default!;
        [JsonConverter(typeof(StringEnumConverter))]
        public PowerplantType Type { get; set; } = default!;
        [JsonProperty(PropertyName = "efficiency")]
        public double Efficiency { get; set; }
        [JsonProperty(PropertyName = "pmin")]
        public int PMin { get; set; }
        [JsonProperty(PropertyName = "pmax")]
        public int PMax { get; set; }
    }
}