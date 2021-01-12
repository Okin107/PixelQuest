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
                    Name = $"{profile.Username}'s current stage",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"**Current stage info**" +
                $"\n XP per min: {UtilityFunctions.FormatNumber(stage.XPPerMinute)}" +
                $" | Coins per min: {UtilityFunctions.FormatNumber(stage.CoinsPerMinute)}" +
                $" | Food chance per min: {stage.FoodPerMinute}%" +
                $"\n Gem chance per min: {stage.GemsDropChancePerMinute}%" +
                $" | Relic chance per min: {stage.RelicsDropChancePerMinute}%" +
                $" | Difficulty: {stage.Difficulty}",
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

            Embed.AddField("Idle Time", $"{DateTime.Now.Second - profile.LastRewardsCollected.Second} seconds", true);
            Embed.AddField("Resources earned", $"-", true);

            return Embed;
        }
    }
}
