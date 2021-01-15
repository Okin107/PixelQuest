using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace IdleHeroes.Models
{
    public static class BotSettings
    {
        public static string Token { get; private set; }
        public static string DevToken { get; private set; }
        public static bool DevMode { get; private set; }
        public static string Prefix { get; private set; }
        public static bool StatusMessages { get; private set; }
        public static bool IsDebugMode { get; private set; }
        public static string DefaultDateFormat { get; private set; }
        public static string DefaultDateTimeFormat { get; private set; }
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
            DevToken = configJson.DevToken;
            DevMode = configJson.DevMode;
            Prefix = configJson.Prefix;
            StatusMessages = configJson.StatusMessages;
            IsDebugMode = configJson.IsDebugMode;
            DefaultDateFormat = configJson.DefaultDateFormat;
            DefaultDateTimeFormat = configJson.DefaultDateTimeFormat;
            BotOwners = configJson.BotOwners;
        }
    }

    public struct ConfigJsonStruct
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
        [JsonProperty("devtoken")]
        public string DevToken { get; private set; }
        [JsonProperty("devmode")]
        public bool DevMode { get; private set; }
        [JsonProperty("prefix")]
        public string Prefix { get; private set; }
        [JsonProperty("statusMessages")]
        public bool StatusMessages { get; private set; }
        [JsonProperty("isdebugmode")]
        public bool IsDebugMode { get; private set; }
        [JsonProperty("defaultdateformat")]
        public string DefaultDateFormat { get; private set; }
        [JsonProperty("defaultdatetimeformat")]
        public string DefaultDateTimeFormat { get; private set; }
        [JsonProperty("owners")]
        public List<ulong> BotOwners { get; private set; }
    }
}
