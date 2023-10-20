using Engie.Contracts.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace Engie.Contracts.Dto
{
    public class PowerplantDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public PowerplantType Type { get; set; } = default!;
        [JsonPropertyName("efficiency")]
        public double Efficiency { get; set; }
        [JsonPropertyName("pmin")]
        public int PMin { get; set; }
        [JsonPropertyName("pmax")]
        public int PMax { get; set; }
    }
}