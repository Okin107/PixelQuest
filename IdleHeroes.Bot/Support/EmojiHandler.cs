using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;

namespace IdleHeroes.Support
{
    public static class EmojiHandler
    {
        private static readonly Dictionary<string, DiscordEmoji> Emojis = new Dictionary<string, DiscordEmoji>();

        public static string GetEmoji(string name)
        {
            return Emojis[name].ToString();
        }

        public static async Task SetupEmojis(DiscordClient client)
        {
            DiscordGuild supportServer = await client.GetGuildAsync(797145881887375427);
            
            var emojiList = supportServer.Emojis.Select(d => d.Value).ToList();
            
            foreach (DiscordEmoji emoji in emojiList)
            {
                Emojis[emoji.Name] = emoji;
            }
        }
    }
}