using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Services;
using IdleHeroes.Support;
using IdleHeroesDAL.Models;
using System;
using System.Linq;

namespace IdleHeroes.EmbedTemplates
{
    public static class StageEmbedTemplate
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
                    Name = $"{profile.Username}'s current stage - Stage {stage.Number}",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"**Current stage info**" +
                $"\nXP per min: {UtilityFunctions.FormatNumber(stage.XPPerMinute)}" +
                $"\nCoins per min: {UtilityFunctions.FormatNumber(stage.CoinsPerMinute)}" +
                $"\nFood chance per min: {stage.FoodPerMinute}%" +
                $"\nGem chance per min: {stage.GemsDropChancePerMinute}%" +
                $"\nRelic chance per min: {stage.RelicsDropChancePerMinute}%" +
                $"\nDifficulty: {stage.Difficulty}",
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

            Embed.AddField("Idle Time", $"{idleTime.ToString("h'h, 'm'm, 's's'")}", true);
            Embed.AddField("Resources found", 
                $"\nXP: {UtilityFunctions.FormatNumber(profile.IdleXP)}" +
                $"\nCoins: {UtilityFunctions.FormatNumber(profile.IdleCoins)}" +
                $"\nFood: {UtilityFunctions.FormatNumber(profile.IdleFood)}" +
                $"\nGems: {UtilityFunctions.FormatNumber(profile.IdleGems)}" +
                $"\nRelics: {UtilityFunctions.FormatNumber(profile.IdleRelics)}", true);

            return Embed;
        }
    }
}
