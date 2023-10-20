using Newtonsoft.Json;

namespace Engie.Domain.Enums
{
    public enum PowerplantType
    {
        [JsonProperty(PropertyName = "gasfired")]
        Gasfired,
        [JsonProperty(PropertyName = "turbojet")]
        Turbojet,
        [JsonProperty(PropertyName = "windturbine")]
        Windturbine
    }
}
