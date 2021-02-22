using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Support;
using IdleHeroesDAL.Models;
using System;

namespace IdleHeroes.EmbedTemplates
{
    public class KeyStoreEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed;

        public static DiscordEmbedBuilder Show(CommandContext ctx, Profile profile, KeyStore store)
        {
            _embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Gold,
                //Title = $"{profile.Username}'s Current Stage",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"Key Store",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"{EmojiHandler.GetEmoji("key")} {UtilityFunctions.FormatNumber(profile.Keys)}" +
                $"\n\nWelcome to the Key Store. Here you can use your {EmojiHandler.GetEmoji("key")} to open chests that contain different companions.",
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            foreach (KeyStoreItem item in store.Items)
            {
                _embed.AddField($"**{item.Id}: {item.Name}**",
                    $"{item.Description}" +
                    $"\n**Tier: {item.Tier}**" +
                    $"\n**Amount: {item.Amount} companions**" +
                    $"\n**Cost: {item.Cost} {EmojiHandler.GetEmoji("key")}**", true);
            }

            return _embed;
        }
    }
}
