using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Models;
using IdleHeroes.Support;
using IdleHeroesDAL.Enums;
using IdleHeroesDAL.Models;
using System;

namespace IdleHeroes.EmbedTemplates
{
    public class KeyStoreTiersEmbedTemplate
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
                Description = $"Welcome to the Key Store." +
                $"\n" +
                $"\nThe Key Store chests have different tiers with different drop rates.",

                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username + $" • Tavern refresh in {timeToRefresh.Hours}:{timeToRefresh.Minutes}:{timeToRefresh.Seconds}"
                }
            };

            _embed.AddField("**Tier 1**",
                $"{EmojiHandler.GetEmoji(RarityTierEnum.Common.ToString().ToLower())} 93.7% Chance" +
                $"\n{EmojiHandler.GetEmoji(RarityTierEnum.Rare.ToString().ToLower())} {CompanionSettings.KeystoreRareChance1}% Chance" +
                $"\n{EmojiHandler.GetEmoji(RarityTierEnum.Epic.ToString().ToLower())} {CompanionSettings.KeystoreEpicChance1}% Chance" +
                $"\n{EmojiHandler.GetEmoji(RarityTierEnum.Legendary.ToString().ToLower())} {CompanionSettings.KeystoreLegendaryChance1}% Chance" +
                $"\n{EmojiHandler.GetEmoji(RarityTierEnum.Mythic.ToString().ToLower())} {CompanionSettings.KeystoreMythicChance1}% Chance", true);

            _embed.AddField("**Tier 2**",
                $"{EmojiHandler.GetEmoji(RarityTierEnum.Rare.ToString().ToLower())} 93.8% Chance" +
                $"\n{EmojiHandler.GetEmoji(RarityTierEnum.Epic.ToString().ToLower())} {CompanionSettings.KeystoreEpicChance2}% Chance" +
                $"\n{EmojiHandler.GetEmoji(RarityTierEnum.Legendary.ToString().ToLower())} {CompanionSettings.KeystoreLegendaryChance2}% Chance" +
                $"\n{EmojiHandler.GetEmoji(RarityTierEnum.Mythic.ToString().ToLower())} {CompanionSettings.KeystoreMythicChance2}% Chance", true);

            _embed.AddField("**Tier 3**",
                $"{EmojiHandler.GetEmoji(RarityTierEnum.Epic.ToString().ToLower())} 94% Chance" +
                $"\n{EmojiHandler.GetEmoji(RarityTierEnum.Legendary.ToString().ToLower())} {CompanionSettings.KeystoreLegendaryChance3}% Chance" +
                $"\n{EmojiHandler.GetEmoji(RarityTierEnum.Mythic.ToString().ToLower())} {CompanionSettings.KeystoreMythicChance3}% Chance", true);

            return _embed;
        }
    }
}
