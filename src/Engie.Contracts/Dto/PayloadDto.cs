using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Engie.Contracts.Dto
{
    public class PayloadDto
    {
        public int Load { get; set; }
        [JsonPropertyName("fuels")]
        public FuelsDto Fuels { get; set; } = default!;
        [JsonPropertyName("powerplants")]
        public PowerplantDto[] Powerplants { get; set; } = default!;
    }
}
