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

            foreach (OwnedCompanions ownedCompanion in profile.OwnedCompanions)
            {
                double levelMultiplierBoost = 1;

                if ((int)ownedCompanion.CompanionAscendTier >= 2)
                {
                    levelMultiplierBoost = Math.Pow(2, Math.Floor((double)ownedCompanion.CompanionAscendTier - 1));
                }

                string tierStarString = UtilityFunctions.GetTierStars((int)ownedCompanion.CompanionAscendTier);

                //Find max level
                double maxLevel = (ownedCompanion.Companion.MaxLevel / 5) * (double)ownedCompanion.CompanionAscendTier;
                double ascendCopiesNeeded = ownedCompanion.Companion.BaseAscendCopiesNeeded * Math.Pow(ownedCompanion.Companion.AscendCopiesTierIncrease, (double)ownedCompanion.CompanionAscendTier - 1);

                if (ownedCompanion.CompanionLevel < maxLevel && ownedCompanion.CompanionAscendTier != AscendTierEnum.Mythic)
                {
                    _embed.AddField($"**{ownedCompanion.Companion.Id}**: {EmojiHandler.GetEmoji(ownedCompanion.Companion.IconName)} {ownedCompanion.Companion.Name}" +
                    $" {EmojiHandler.GetEmoji(ownedCompanion.Companion.AscendTier.ToString().ToLower())}",
                    $"\n{EmojiHandler.GetEmoji(ownedCompanion.Companion.Element.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.Class.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.DamageType.ToString().ToLower())} " +
                    $"\nLv: {ownedCompanion.CompanionLevel}/{maxLevel}" +
                    $"\n" +
                    $"\n**Attributes -> Next Lv.**" +
                    $"\nTier: {tierStarString}" +
                    $"\nDPS: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.DPS * Math.Pow(ownedCompanion.Companion.DPSIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> {UtilityFunctions.FormatNumber(ownedCompanion.Companion.DPS * Math.Pow(ownedCompanion.Companion.DPSIncreasePerLevel, ownedCompanion.CompanionLevel) * levelMultiplierBoost)}" +
                    $"\nHP: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.HP * Math.Pow(ownedCompanion.Companion.HPIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> {UtilityFunctions.FormatNumber(ownedCompanion.Companion.HP * Math.Pow(ownedCompanion.Companion.HPIncreasePerLevel, ownedCompanion.CompanionLevel) * levelMultiplierBoost)}" +
                    $"\nArmor: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Armor * Math.Pow(ownedCompanion.Companion.ArmorIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Armor * Math.Pow(ownedCompanion.Companion.ArmorIncreasePerLevel, ownedCompanion.CompanionLevel) * levelMultiplierBoost)}" +
                    $"\nAccuracy: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Accuracy * Math.Pow(ownedCompanion.Companion.AccuracyIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Accuracy * Math.Pow(ownedCompanion.Companion.AccuracyIncreasePerLevel, ownedCompanion.CompanionLevel) * levelMultiplierBoost)}" +
                    $"\nAgility: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Agility * Math.Pow(ownedCompanion.Companion.AgilityIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Agility * Math.Pow(ownedCompanion.Companion.AgilityIncreasePerLevel, ownedCompanion.CompanionLevel) * levelMultiplierBoost)}" +
                    $"\n" +
                     $"\n**Upgrade**" +
                    $"\nLevel: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.BaseLevelCost * Math.Pow(ownedCompanion.Companion.LevelCostIncrease, ownedCompanion.CompanionLevel - 1))} {EmojiHandler.GetEmoji("coin")}" +
                    $"\nAscend: { ownedCompanion.CompanionCopies}/{ ascendCopiesNeeded} Copies", true);
                }
                else if (ownedCompanion.CompanionLevel < maxLevel && ownedCompanion.CompanionAscendTier == AscendTierEnum.Mythic)
                {
                    _embed.AddField($"**{ownedCompanion.Companion.Id}**: {EmojiHandler.GetEmoji(ownedCompanion.Companion.IconName)} {ownedCompanion.Companion.Name}" +
                    $" {EmojiHandler.GetEmoji(ownedCompanion.Companion.AscendTier.ToString().ToLower())}",
                    $"\n{EmojiHandler.GetEmoji(ownedCompanion.Companion.Element.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.Class.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.DamageType.ToString().ToLower())} " +
                    $"\nLv: {ownedCompanion.CompanionLevel}/{maxLevel}" +
                    $"\n" +
                    $"\n**Attributes -> Next Lv.**" +
                    $"\nTier: {tierStarString}" +
                    $"\nDPS: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.DPS * Math.Pow(ownedCompanion.Companion.DPSIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> {UtilityFunctions.FormatNumber(ownedCompanion.Companion.DPS * Math.Pow(ownedCompanion.Companion.DPSIncreasePerLevel, ownedCompanion.CompanionLevel) * levelMultiplierBoost)}" +
                    $"\nHP: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.HP * Math.Pow(ownedCompanion.Companion.HPIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> {UtilityFunctions.FormatNumber(ownedCompanion.Companion.HP * Math.Pow(ownedCompanion.Companion.HPIncreasePerLevel, ownedCompanion.CompanionLevel) * levelMultiplierBoost)}" +
                    $"\nArmor: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Armor * Math.Pow(ownedCompanion.Companion.ArmorIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Armor * Math.Pow(ownedCompanion.Companion.ArmorIncreasePerLevel, ownedCompanion.CompanionLevel) * levelMultiplierBoost)}" +
                    $"\nAccuracy: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Accuracy * Math.Pow(ownedCompanion.Companion.AccuracyIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Accuracy * Math.Pow(ownedCompanion.Companion.AccuracyIncreasePerLevel, ownedCompanion.CompanionLevel) * levelMultiplierBoost)}" +
                    $"\nAgility: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Agility * Math.Pow(ownedCompanion.Companion.AgilityIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Agility * Math.Pow(ownedCompanion.Companion.AgilityIncreasePerLevel, ownedCompanion.CompanionLevel) * levelMultiplierBoost)}" +
                    $"\n" +
                     $"\n**Upgrade**" +
                    $"\nLevel: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.BaseLevelCost * Math.Pow(ownedCompanion.Companion.LevelCostIncrease, ownedCompanion.CompanionLevel - 1))} {EmojiHandler.GetEmoji("coin")}" +
                    $"\nAscend: MAX", true);
                }
                else if (ownedCompanion.CompanionLevel == maxLevel && ownedCompanion.CompanionAscendTier != AscendTierEnum.Mythic)
                {
                    _embed.AddField($"**{ownedCompanion.Companion.Id}**: {EmojiHandler.GetEmoji(ownedCompanion.Companion.IconName)} {ownedCompanion.Companion.Name}" +
                    $" {EmojiHandler.GetEmoji(ownedCompanion.Companion.AscendTier.ToString().ToLower())}",
                    $"\n{EmojiHandler.GetEmoji(ownedCompanion.Companion.Element.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.Class.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.DamageType.ToString().ToLower())} " +
                    $"\nLv: {ownedCompanion.CompanionLevel}/{maxLevel}" +
                    $"\n" +
                    $"\n**Attributes -> Next Lv.**" +
                    $"\nTier: {tierStarString}" +
                    $"\nDPS: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.DPS * Math.Pow(ownedCompanion.Companion.DPSIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> MAX" +
                    $"\nHP: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.HP * Math.Pow(ownedCompanion.Companion.HPIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> MAX" +
                    $"\nArmor: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Armor * Math.Pow(ownedCompanion.Companion.ArmorIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> MAX" +
                    $"\nAccuracy: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Accuracy * Math.Pow(ownedCompanion.Companion.AccuracyIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> MAX" +
                    $"\nAgility: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Agility * Math.Pow(ownedCompanion.Companion.AgilityIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> MAX" +
                    $"\n" +
                    $"\n**Upgrade**" +
                    $"\nAscend: {ownedCompanion.CompanionCopies}/{ascendCopiesNeeded} Copies", true);
                }
                else
                {
                    _embed.AddField($"**{ownedCompanion.Companion.Id}**: {EmojiHandler.GetEmoji(ownedCompanion.Companion.IconName)} {ownedCompanion.Companion.Name}" +
                    $" {EmojiHandler.GetEmoji(ownedCompanion.Companion.AscendTier.ToString().ToLower())}",
                    $"\n{EmojiHandler.GetEmoji(ownedCompanion.Companion.Element.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.Class.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.DamageType.ToString().ToLower())} " +
                    $"\nLv: {ownedCompanion.CompanionLevel}/{maxLevel}" +
                    $"\n" +
                    $"\n**Attributes -> Next Lv.**" +
                    $"\nTier: {tierStarString}" +
                    $"\nDPS: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.DPS * Math.Pow(ownedCompanion.Companion.DPSIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> MAX" +
                    $"\nHP: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.HP * Math.Pow(ownedCompanion.Companion.HPIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> MAX" +
                    $"\nArmor: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Armor * Math.Pow(ownedCompanion.Companion.ArmorIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> MAX" +
                    $"\nAccuracy: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Accuracy * Math.Pow(ownedCompanion.Companion.AccuracyIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
                    $" -> MAX" +
                    $"\nAgility: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Agility * Math.Pow(ownedCompanion.Companion.AgilityIncreasePerLevel, ownedCompanion.CompanionLevel - 1) * levelMultiplierBoost)}" +
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
