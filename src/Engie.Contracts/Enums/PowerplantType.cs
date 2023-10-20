using Newtonsoft.Json;

namespace Engie.Contracts.Enums
{
    public enum PowerplantType
    {
        [JsonProperty("gasfired")]
        Gasfired,
        [JsonProperty("turbojet")]
        Turbojet,
        [JsonProperty("windturbine")]
        Windturbine
    }
}
