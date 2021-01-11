using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace IdleHeroes.Models
{
    public static class BotSettings
    {
        public static string Token { get; private set; }
        public static string Prefix { get; private set; }
        public static bool StatusMessages { get; private set; }
        public static bool IsDebugMode { get; private set; }
        public static List<ulong> BotOwners { get; private set; }

        public static void Initialize()
        {
            string configString;

            using (var fs = File.OpenRead("config.json"))
            {
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                {
                    configString = sr.ReadToEnd();
                }
            }

            ConfigJsonStruct configJson = JsonConvert.DeserializeObject<ConfigJsonStruct>(configString);

            //Map the attributes
            Token = configJson.Token;
            Prefix = configJson.Prefix;
            StatusMessages = configJson.StatusMessages;
            BotOwners = configJson.BotOwners;
        }
    }

    public struct ConfigJsonStruct
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
        [JsonProperty("prefix")]
        public string Prefix { get; private set; }
        [JsonProperty("statusMessages")]
        public bool StatusMessages { get; private set; }
        [JsonProperty("isdebugmode")]
        public static bool IsDebugMode { get; private set; }
        [JsonProperty("owners")]
        public List<ulong> BotOwners { get; private set; }
    }
}
