using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Support;
using IdleHeroesDAL.Models;
using System;

namespace IdleHeroes.EmbedTemplates
{
    public static class FarmEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed;

        public static DiscordEmbedBuilder Show(CommandContext ctx, Profile profile)
        {
            _embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Green,
                //Title = $"{profile.Username}'s Current Stage",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"{profile.Username} - Stage {profile.Stage.Number}",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"Use `.farm collect` to collect your resources.",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail()
                {
                    Url = ctx.Message.Author.AvatarUrl
                },
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            TimeSpan idleTime = UtilityFunctions.GetIdleDisplayTime(profile);
            
            _embed.AddField("Resources found", 
                $"\n{EmojiHandler.GetEmoji("xp")} {UtilityFunctions.FormatNumber(profile.IdleXP)}" +
                $"\n{EmojiHandler.GetEmoji("coin")} {UtilityFunctions.FormatNumber(profile.IdleCoins)}" +
                $"\n{EmojiHandler.GetEmoji("food")} {UtilityFunctions.FormatNumber(profile.IdleFood)}" +
                $"\n{EmojiHandler.GetEmoji("gem")} {UtilityFunctions.FormatNumber(profile.IdleGems)}" +
                $"\n{EmojiHandler.GetEmoji("relic")} {UtilityFunctions.FormatNumber(profile.IdleRelics)}", true);

            _embed.AddField("Idle Time", $"{idleTime.ToString("h'h, 'm'm, 's's'")}", true);

            return _embed;
        }
    }
}
