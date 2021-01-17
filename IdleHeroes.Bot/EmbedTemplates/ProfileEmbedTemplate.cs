using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Models;
using IdleHeroes.Support;
using IdleHeroesDAL.Models;
using System;

namespace IdleHeroes.EmbedTemplates
{
    public static class ProfileEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed = null;

        public static DiscordEmbedBuilder Get(CommandContext ctx, Profile profile)
        {
            _embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Turquoise,
                //Title = $"{profile.Username}'s profile",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"{profile.Username}'s profile ({profile.DiscordName})",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                //Description = $"**Discord Name**: ",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail()
                {
                    Url = ctx.Message.Author.AvatarUrl
                },
                //ImageUrl = ctx.Message.Author.AvatarUrl,
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            

            _embed.AddField("Resources", 
                $"{EmojiHandler.GetEmoji("coin")} {UtilityFunctions.FormatNumber(profile.Coins)}" +
                $"\n{EmojiHandler.GetEmoji("food")} {UtilityFunctions.FormatNumber(profile.Food)}" +
                $"\n{EmojiHandler.GetEmoji("gem")} {UtilityFunctions.FormatNumber(profile.Gems)}" +
                $"\n{EmojiHandler.GetEmoji("relic")} {UtilityFunctions.FormatNumber(profile.Relics)}", true);

            _embed.AddField("Hero", 
                $"{EmojiHandler.GetEmoji("lvl")} {profile.Level}" +
                $"\n{EmojiHandler.GetEmoji("xp")} {UtilityFunctions.FormatNumber(profile.XP)}" +
                $"\n⚔️ {UtilityFunctions.FormatNumber(profile.BaseDPS)}", true);

            _embed.AddField("Stage", $"Nr: {profile.Stage.Number}" +
                $"\nDifficulty: {profile.Stage.Difficulty}", true);

            _embed.AddField("Registered", $"{profile.RegisteredOn.ToString(BotSettings.DefaultDateFormat)}", true);
            _embed.AddField("Max Idle Time", $"{profile.MaximumIdleRewardHours} hours", true);
            _embed.AddField("Last Played", $"{UtilityFunctions.GetRelativeTime(profile.LastPlayed)}", true);

            return _embed;
        }
    }
}
