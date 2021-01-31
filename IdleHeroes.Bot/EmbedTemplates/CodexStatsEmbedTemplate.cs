using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Support;
using IdleHeroesDAL.Enums;
using IdleHeroesDAL.Models;
using System;
using System.Collections.Generic;

namespace IdleHeroes.EmbedTemplates
{
    public static class CodexStatsEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed;

        public static DiscordEmbedBuilder Show(CommandContext ctx)
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
                Description = $"**Companion icons:**" +
                $"\nRarity: {EmojiHandler.GetEmoji(RarityTierEnum.Common.ToString().ToLower())} Common, " +
                $"{EmojiHandler.GetEmoji(RarityTierEnum.Rare.ToString().ToLower())} Rare, " +
                $"{EmojiHandler.GetEmoji(RarityTierEnum.Epic.ToString().ToLower())} Epic, " +
                $"{EmojiHandler.GetEmoji(RarityTierEnum.Legendary.ToString().ToLower())} Legendary, " +
                $"{EmojiHandler.GetEmoji(RarityTierEnum.Mythic.ToString().ToLower())} Mythic." +
                $"\nElements: {EmojiHandler.GetEmoji(ElementTypeEnum.Nature.ToString().ToLower())} Nature, " +
                $"{EmojiHandler.GetEmoji(ElementTypeEnum.Water.ToString().ToLower())} Water, " +
                $"{EmojiHandler.GetEmoji(ElementTypeEnum.Fire.ToString().ToLower())} Fire. " +
                $"\nClasses: {EmojiHandler.GetEmoji(CompanionClassesEnum.Warrior.ToString().ToLower())} Warrior, " +
                $"{EmojiHandler.GetEmoji(CompanionClassesEnum.Ranger.ToString().ToLower())} Ranger, " +
                $"{EmojiHandler.GetEmoji(CompanionClassesEnum.Tank.ToString().ToLower())} Tank, " +
                $"{EmojiHandler.GetEmoji(CompanionClassesEnum.Assasin.ToString().ToLower())} Assasin." +
                $"\nDMG Types: {EmojiHandler.GetEmoji(DamageTypeEnum.Melee.ToString().ToLower())} Melee, " +
                $"{EmojiHandler.GetEmoji(DamageTypeEnum.Ranged.ToString().ToLower())} Ranged. " +
                $"\nAscend Tiers: {UtilityFunctions.GetTierStars(5)}",
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            //foreach (Companion companion in companions)
            //{
            //    _embed.AddField($"**{companion.Id}**: {EmojiHandler.GetEmoji(companion.IconName)} {companion.Name} " +
            //    $"{EmojiHandler.GetEmoji(companion.RarityTier.ToString().ToLower())}",
            //    $"\n{EmojiHandler.GetEmoji(companion.Element.ToString().ToLower())} " +
            //    $"{EmojiHandler.GetEmoji(companion.Class.ToString().ToLower())} " +
            //    $"{EmojiHandler.GetEmoji(companion.DamageType.ToString().ToLower())} " +
            //    $"\nLv: {companion.MaxLevel}" +
            //    $"\n{companion.Lore}" +
            //    $"\n" +
            //    $"\n**Attributes**" +
            //    $"\nTier: {UtilityFunctions.GetTierStars(5)}" +
            //    $"\nDPS: {CompanionHelper.CalculateAttributeString(companion, CompanionAttributeEnum.DPS)}" +
            //    $"\nHP: {CompanionHelper.CalculateAttributeString(companion, CompanionAttributeEnum.HP)}" +
            //    $"\nArmor: {CompanionHelper.CalculateAttributeString(companion, CompanionAttributeEnum.Armor)}" +
            //    $"\nAccuracy: {CompanionHelper.CalculateAttributeString(companion, CompanionAttributeEnum.Accuracy)}" +
            //    $"\nAgility: {CompanionHelper.CalculateAttributeString(companion, CompanionAttributeEnum.Agility)}", true);
            //}

            return _embed;
        }
    }
}
