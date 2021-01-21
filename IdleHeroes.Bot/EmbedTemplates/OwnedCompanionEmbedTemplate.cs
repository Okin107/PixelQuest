using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Support;
using IdleHeroesDAL.Enums;
using IdleHeroesDAL.Models;
using System;

namespace IdleHeroes.EmbedTemplates
{
    public class OwnedCompanionsEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed;

        public static DiscordEmbedBuilder Show(CommandContext ctx, Profile profile)
        {
            _embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Brown,
                //Title = $"{profile.Username}'s Current Stage",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"{profile.Username}'s Companions",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"{EmojiHandler.GetEmoji("coin")} {UtilityFunctions.FormatNumber(profile.Coins)}" +
                $" • {EmojiHandler.GetEmoji("food")} {UtilityFunctions.FormatNumber(profile.Food)}" +
                $"\n\nThis is your Companions page. Here you can manage all of your Companions.",
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            if (profile.OwnedCompanions.Count == 0)
            {
                _embed.Description += "\n\nYou do not have any Companions yet. You can hire some from the Tavern(`.tavern`).";
            }

            foreach (OwnedCompanion ownedCompanion in profile.OwnedCompanions)
            {
                if (ownedCompanion.Level < CompanionHelper.GetMaxLevel(ownedCompanion) && ownedCompanion.RarirtyTier != RarityTierEnum.Mythic)
                {
                    _embed.AddField($"**{ownedCompanion.Companion.Id}**: {EmojiHandler.GetEmoji(ownedCompanion.Companion.IconName)} {ownedCompanion.Companion.Name}" +
                    $" {EmojiHandler.GetEmoji(ownedCompanion.Companion.RarityTier.ToString().ToLower())}",
                    $"\n{EmojiHandler.GetEmoji(ownedCompanion.Companion.Element.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.Class.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.DamageType.ToString().ToLower())} " +
                    $"\nLv: {ownedCompanion.Level}/{CompanionHelper.GetMaxLevel(ownedCompanion)}" +
                    $"\n" +
                    $"\n**Attributes -> Next Lv.**" +
                    $"\nTier: {UtilityFunctions.GetTierStars((int)ownedCompanion.RarirtyTier)}" +
                    $"\nDPS: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.DPS)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.DPS, true)}" +
                    $"\nHP: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.HP)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.HP, true)}" +
                    $"\nArmor: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Armor)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Armor, true)}" +
                    $"\nAccuracy: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Accuracy)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Accuracy, true)}" +
                    $"\nAgility: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Agility)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Agility, true)}" +
                    $"\n" +
                     $"\n**Upgrade**" +
                    $"\nLevel: {CompanionHelper.NextLevelCost(ownedCompanion)} {EmojiHandler.GetEmoji("coin")}" +
                    $"\nAscend: {ownedCompanion.Copies}/{CompanionHelper.GetAscendCopiesNeeded(ownedCompanion)} Copies", true);
                }
                else if (ownedCompanion.Level < CompanionHelper.GetMaxLevel(ownedCompanion) && ownedCompanion.RarirtyTier == RarityTierEnum.Mythic)
                {
                    _embed.AddField($"**{ownedCompanion.Companion.Id}**: {EmojiHandler.GetEmoji(ownedCompanion.Companion.IconName)} {ownedCompanion.Companion.Name}" +
                    $" {EmojiHandler.GetEmoji(ownedCompanion.Companion.RarityTier.ToString().ToLower())}",
                    $"\n{EmojiHandler.GetEmoji(ownedCompanion.Companion.Element.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.Class.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.DamageType.ToString().ToLower())} " +
                    $"\nLv: {ownedCompanion.Level}/{CompanionHelper.GetMaxLevel(ownedCompanion)}" +
                    $"\n" +
                    $"\n**Attributes -> Next Lv.**" +
                    $"\nTier: {UtilityFunctions.GetTierStars((int)ownedCompanion.RarirtyTier)}" +
                    $"\nDPS: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.DPS)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.DPS, true)}" +
                    $"\nHP: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.HP)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.HP, true)}" +
                    $"\nArmor: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Armor)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Armor, true)}" +
                    $"\nAccuracy: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Accuracy)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Accuracy, true)}" +
                    $"\nAgility: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Agility)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Agility, true)}" +
                    $"\n" +
                     $"\n**Upgrade**" +
                    $"\nLevel: {CompanionHelper.NextLevelCost(ownedCompanion)} {EmojiHandler.GetEmoji("coin")}" +
                    $"\nAscend: MAX", true);
                }
                else if (ownedCompanion.Level == CompanionHelper.GetMaxLevel(ownedCompanion) && ownedCompanion.RarirtyTier != RarityTierEnum.Mythic)
                {
                    _embed.AddField($"**{ownedCompanion.Companion.Id}**: {EmojiHandler.GetEmoji(ownedCompanion.Companion.IconName)} {ownedCompanion.Companion.Name}" +
                    $" {EmojiHandler.GetEmoji(ownedCompanion.Companion.RarityTier.ToString().ToLower())}",
                    $"\n{EmojiHandler.GetEmoji(ownedCompanion.Companion.Element.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.Class.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.DamageType.ToString().ToLower())} " +
                    $"\nLv: {ownedCompanion.Level}/{CompanionHelper.GetMaxLevel(ownedCompanion)}" +
                    $"\n" +
                    $"\n**Attributes -> Next Lv.**" +
                    $"\nTier: {UtilityFunctions.GetTierStars((int)ownedCompanion.RarirtyTier)}" +
                    $"\nDPS: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.DPS)}" +
                    $" -> MAX" +
                    $"\nHP: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.HP)}" +
                    $" -> MAX" +
                    $"\nArmor: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Armor)}" +
                    $" -> MAX" +
                    $"\nAccuracy: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Accuracy)}" +
                    $" -> MAX" +
                     $"\nAgility: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Agility)}" +
                    $" -> MAX" +
                    $"\n" +
                    $"\n**Upgrade**" +
                    $"\nAscend: {ownedCompanion.Copies}/{CompanionHelper.GetAscendCopiesNeeded(ownedCompanion)} Copies", true);
                }
                else
                {
                    _embed.AddField($"**{ownedCompanion.Companion.Id}**: {EmojiHandler.GetEmoji(ownedCompanion.Companion.IconName)} {ownedCompanion.Companion.Name}" +
                    $" {EmojiHandler.GetEmoji(ownedCompanion.Companion.RarityTier.ToString().ToLower())}",
                    $"\n{EmojiHandler.GetEmoji(ownedCompanion.Companion.Element.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.Class.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.DamageType.ToString().ToLower())} " +
                    $"\nLv: {ownedCompanion.Level}/{CompanionHelper.GetMaxLevel(ownedCompanion)}" +
                    $"\n" +
                    $"\n**Attributes -> Next Lv.**" +
                    $"\nTier: {UtilityFunctions.GetTierStars((int)ownedCompanion.RarirtyTier)}" +
                    $"\nDPS: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.DPS)}" +
                    $" -> MAX" +
                    $"\nHP: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.HP)}" +
                    $" -> MAX" +
                    $"\nArmor: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Armor)}" +
                    $" -> MAX" +
                    $"\nAccuracy: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Accuracy)}" +
                    $" -> MAX" +
                     $"\nAgility: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Agility)}" +
                    $" -> MAX" +
                    $"\n" +
                    $"\n**Upgrade**" +
                    $"\nAscend: MAX", true);
                }


            }

            return _embed;
        }
    }
}
