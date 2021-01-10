using System.Collections.Generic;
using Newtonsoft.Json;

namespace IdleHeroes.Models
{
    public struct ConfigModel
    {
        [JsonProperty("token")]
        public string Token { get; private set; }

        [JsonProperty("prefix")]
        public string Prefix { get; private set; }
        [JsonProperty("statusMessages")]
        public bool StatusMessages { get; private set; }
        [JsonProperty("owners")]
        public List<ulong> BotOwners { get; private set; }
    }
}
