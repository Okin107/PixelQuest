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

        public static DiscordEmoji GetEmoji(string name)
        {
            DiscordEmoji emojiFound = Emojis.Find(x => x.Name.Substring(4).Equals(name));

            return emojiFound;
        }

        public static void SetupEmojis(DiscordClient client)
        {
            List<DiscordEmoji> emojiList = new List<DiscordEmoji>();

            foreach (ulong serverId in BotSettings.SupportServersId)
            {
                DiscordGuild supportServer = client.Guilds.FirstOrDefault(x => x.Key == serverId).Value;

                emojiList.AddRange(supportServer.Emojis.Where(x => x.Value.Name.StartsWith("bot_") && !x.Value.Name.EndsWith("_old")).Select(x => x.Value));
            }

            Emojis.AddRange(emojiList);
        }
    }
}