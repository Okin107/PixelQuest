using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Services;
using IdleHeroes.Support;
using IdleHeroesDAL.Models;
using System;
using System.Linq;

namespace IdleHeroes.EmbedTemplates
{
    public static class StageInfoEmbedTemplate
    {
        private static DiscordEmbedBuilder Embed = null;

        public static DiscordEmbedBuilder Show(CommandContext ctx, Profile profile, Stage stage)
        {
            Embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Turquoise,
                //Title = $"{profile.Username}'s Current Stage",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"{profile.Username} - Stage {stage.Number}",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"**Stage info**" +
                $"\nDifficulty: {stage.Difficulty}" +
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_xp")} {UtilityFunctions.FormatNumber(stage.XPPerMinute)} per min" +
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_coin")} {UtilityFunctions.FormatNumber(stage.CoinsPerMinute)} per min" +
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_food")} {stage.FoodPerMinute}% for 1 per min" +
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_gem")} {stage.GemsDropChancePerMinute}% for 1 per min" +
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_relic")} {stage.RelicsDropChancePerMinute}% for 1 per min"
                ,
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

            return Embed;
        }
    }
}
