using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Support;
using IdleHeroesDAL.Enums;
using IdleHeroesDAL.Models;
using System;
using System.Collections.Generic;

namespace IdleHeroes.EmbedTemplates
{
    public static class CodexEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed;

        public static DiscordEmbedBuilder Show(CommandContext ctx, List<Companion> companions)
        {
            _embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Brown,
                //Title = $"{profile.Username}'s Current Stage",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"Companions codex",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"**Icons description:**" +
                $"\nRarity: {EmojiHandler.GetEmoji(AscendTierEnum.Common.ToString().ToLower())} Common, " +
                $"{EmojiHandler.GetEmoji(AscendTierEnum.Rare.ToString().ToLower())} Rare, " +
                $"{EmojiHandler.GetEmoji(AscendTierEnum.Epic.ToString().ToLower())} Epic, " +
                $"{EmojiHandler.GetEmoji(AscendTierEnum.Legendary.ToString().ToLower())} Legendary, " +
                $"{EmojiHandler.GetEmoji(AscendTierEnum.Mythic.ToString().ToLower())} Mythic." +
                $"\nElements: {EmojiHandler.GetEmoji(ElementTypeEnum.Nature.ToString().ToLower())} Nature, " +
                $"{EmojiHandler.GetEmoji(ElementTypeEnum.Water.ToString().ToLower())} Water, " +
                $"{EmojiHandler.GetEmoji(ElementTypeEnum.Fire.ToString().ToLower())} Fire. " +
                $"\nClasses: {EmojiHandler.GetEmoji(CompanionClassesEnum.Warrior.ToString().ToLower())} Warrior, " +
                $"{EmojiHandler.GetEmoji(CompanionClassesEnum.Ranger.ToString().ToLower())} Ranger, " +
                $"{EmojiHandler.GetEmoji(CompanionClassesEnum.Tank.ToString().ToLower())} Tank, " +
                $"{EmojiHandler.GetEmoji(CompanionClassesEnum.Support.ToString().ToLower())} Support." +
                $"\nDMG Types: {EmojiHandler.GetEmoji(DamageTypeEnum.Melee.ToString().ToLower())} Melee, " +
                $"{EmojiHandler.GetEmoji(DamageTypeEnum.Ranged.ToString().ToLower())} Ranged. " +
                $"\nAscend Tiers: {UtilityFunctions.GetTierStars(5)}" +
                $"\n" +
                $"\n" +
                $"Here you can preview all the companions at their maximum level.",
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            foreach (Companion companion in companions)
            {
                _embed.AddField($"**{companion.Id}**: {EmojiHandler.GetEmoji(companion.IconName)} {companion.Name} " +
                $"{EmojiHandler.GetEmoji(companion.AscendTier.ToString().ToLower())}",
                $"\n{EmojiHandler.GetEmoji(companion.Element.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(companion.Class.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(companion.DamageType.ToString().ToLower())} " +
                $"\nLv: {companion.MaxLevel}" +
                $"\n{companion.Lore}" +
                $"\n" +
                $"\n**Attributes**" +
                $"\nTier: {UtilityFunctions.GetTierStars(5)}" +
                $"\nDPS: {UtilityFunctions.FormatNumber(companion.DPS * Math.Pow(companion.DPSIncreasePerLevel, companion.MaxLevel - 1) * Math.Pow(2, (companion.MaxLevel / companion.LevelToMultiplyIncreases) - 1))}" +
                $"\nHP: {UtilityFunctions.FormatNumber(companion.HP * Math.Pow(companion.HPIncreasePerLevel, companion.MaxLevel - 1) * Math.Pow(2, (companion.MaxLevel / companion.LevelToMultiplyIncreases) - 1))}" +
                $"\nArmor: {UtilityFunctions.FormatNumber(companion.Armor * Math.Pow(companion.ArmorIncreasePerLevel, companion.MaxLevel - 1) * Math.Pow(2, (companion.MaxLevel / companion.LevelToMultiplyIncreases) - 1))}" +
                $"\nAccuracy: {UtilityFunctions.FormatNumber(companion.Accuracy * Math.Pow(companion.AccuracyIncreasePerLevel, companion.MaxLevel - 1) * Math.Pow(2, (companion.MaxLevel / companion.LevelToMultiplyIncreases) - 1))}" +
                $"\nAgility: {UtilityFunctions.FormatNumber(companion.Agility * Math.Pow(companion.AgilityIncreasePerLevel, companion.MaxLevel - 1) * Math.Pow(2, (companion.MaxLevel / companion.LevelToMultiplyIncreases) - 1))}", true);
            }

            return _embed;
        }
    }
}
