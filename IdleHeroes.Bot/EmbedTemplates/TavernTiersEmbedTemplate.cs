using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Models;
using IdleHeroes.Support;
using IdleHeroesDAL.Models;
using System;

namespace IdleHeroes.EmbedTemplates
{
    public class TavernTiersEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed;

        public static DiscordEmbedBuilder Show(CommandContext ctx, Profile profile)
        {
            TimeSpan timeToRefresh = DateTime.Today.AddDays(1) - DateTime.Now;

            _embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Gold,
                //Title = $"{profile.Username}'s Current Stage",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"Tavern tiers",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"Welcome to the Tavern. Here you can meet and hire Companions to help you in your journey." +
                $"\n" +
                $"\nThe Tavern has different tiers which allows you to access higher rarities of each companion.",

                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username + $" • Tavern refresh in {timeToRefresh.Hours}:{timeToRefresh.Minutes}:{timeToRefresh.Seconds}"
                }
            };

            _embed.AddField("**Tier 1**",
                "Common: 100% Chance", true);

            _embed.AddField("**Tier 2**",
                $"Common: 80% Chance" +
                $"\nRare: {CompanionSettings.TavernRareChance}% Chance", true);

            _embed.AddField("**Tier 3**",
                $"Common: 65% Chance" +
                $"\nRare: {CompanionSettings.TavernRareChance}% Chance" +
                $"\nEpic: {CompanionSettings.TavernEpicChance}% Chance", true);

            _embed.AddField("**Tier 4**",
                $"Common: 55% Chance" +
                $"\nRare: {CompanionSettings.TavernRareChance}% Chance" +
                $"\nEpic: {CompanionSettings.TavernEpicChance}% Chance" +
                $"\nLegendary: {CompanionSettings.TavernLegendaryChance}% Chance", true);

            _embed.AddField("**Tier 5**",
                $"Common: 50% Chance" +
                $"\nRare: {CompanionSettings.TavernRareChance}% Chance" +
                $"\nEpic: {CompanionSettings.TavernEpicChance}% Chance" +
                $"\nLegendary: {CompanionSettings.TavernLegendaryChance}% Chance" +
                $"\nMythic: {CompanionSettings.TavernMythicChance}% Chance", true);

            _embed.AddField("**Tier 6**",
                $"Rare: 70% Chance" +
                $"\nEpic: {CompanionSettings.TavernEpicChance}% Chance" +
                $"\nLegendary: {CompanionSettings.TavernLegendaryChance}% Chance" +
                $"\nMythic: {CompanionSettings.TavernMythicChance}% Chance", true);

            _embed.AddField("**Tier 7**",
                $"Epic: 85% Chance" +
                $"\nLegendary: {CompanionSettings.TavernLegendaryChance}% Chance" +
                $"\nMythic: {CompanionSettings.TavernMythicChance}% Chance", true);

            return _embed;
        }
    }
}
