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
                $"{UtilityFunctions.GetEmoji(ctx, "bot_coin")} {UtilityFunctions.FormatNumber(profile.Coins)}" +
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_food")} {UtilityFunctions.FormatNumber(profile.Food)}" +
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_gem")} {UtilityFunctions.FormatNumber(profile.Gems)}" +
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_relic")} {UtilityFunctions.FormatNumber(profile.Relics)}", true);

            _embed.AddField("Level & DPS", 
                $"Level: {profile.Level}" +
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_xp")} {UtilityFunctions.FormatNumber(profile.XP)}" +
                $"\nHero DPS: {UtilityFunctions.FormatNumber(profile.BaseDPS)}", true);

            _embed.AddField("Stage Info", $"Number: {profile.Stage.Number}", true);

            _embed.AddField("Registered", $"{profile.RegisteredOn.ToString(BotSettings.DefaultDateFormat)}", true);
            _embed.AddField("Max Idle Time", $"{profile.MaximumIdleRewardHours} hours", true);
            _embed.AddField("Last Played", $"{UtilityFunctions.GetRelativeTime(profile.LastPlayed)}", true);

            return _embed;
        }
    }
}
