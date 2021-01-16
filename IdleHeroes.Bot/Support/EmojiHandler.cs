using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using IdleHeroes.Models;

namespace IdleHeroes.Support
{
    public static class EmojiHandler
    {
        private static readonly List<DiscordEmoji> Emojis = new List<DiscordEmoji>();

        public static string GetEmoji(string name)
        {
            return Emojis.Find(x => x.Name.Equals(name));
        }

        public static void SetupEmojis(DiscordClient client)
        {
            List<DiscordEmoji> emojiList = new List<DiscordEmoji>();

            foreach (ulong serverId in BotSettings.SupportServersId)
            {
                DiscordGuild supportServer = client.Guilds.FirstOrDefault(x => x.Key == serverId).Value;

                emojiList.AddRange(supportServer.Emojis.Select(d => d.Value).ToList());
            }

            Emojis.AddRange(emojiList);
        }
    }
}