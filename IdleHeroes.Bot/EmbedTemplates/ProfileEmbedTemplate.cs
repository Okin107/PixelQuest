using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Models;
using IdleHeroes.Support;
using IdleHeroesDAL.Models;
using System;
using System.Linq;

namespace IdleHeroes.EmbedTemplates
{
    public static class ProfileEmbedTemplate
    {
        private static DiscordEmbedBuilder Embed = null;

        public static DiscordEmbedBuilder Get(CommandContext ctx, Profile profile)
        {
            Embed = new DiscordEmbedBuilder()
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

            

            Embed.AddField("Resources", 
                $"{UtilityFunctions.GetEmoji(ctx, "bot_coin")} {UtilityFunctions.FormatNumber(profile.Coins)}" +
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_food")} {UtilityFunctions.FormatNumber(profile.Food)}" +
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_gem")} {UtilityFunctions.FormatNumber(profile.Gems)}" +
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_relic")} {UtilityFunctions.FormatNumber(profile.Relics)}", true);

            Embed.AddField("Level & DPS", 
                $"Level: {profile.Level}" +
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_xp")} {UtilityFunctions.FormatNumber(profile.XP)}" +
                $"\nHero DPS: {UtilityFunctions.FormatNumber(profile.BaseDPS)}", true);

            Embed.AddField("Stage Info", $"Number: {profile.CurrentStageNumber}", true);

            Embed.AddField("Registered", $"{profile.RegisteredOn.ToString(BotSettings.DefaultDateFormat)}", true);
            Embed.AddField("Max Idle Time", $"{profile.MaximumIdleRewardHours} hours", true);
            Embed.AddField("Last Played", $"{UtilityFunctions.GetRelativeTime(profile.LastPlayed)}", true);

            return Embed;
        }
    }
}
