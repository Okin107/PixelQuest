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
                    Name = $"Stats codex",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"**Companion icons:**" +
                $"\nRarity: {EmojiHandler.GetEmoji(RarityTierEnum.Common.ToString().ToLower())} Common, " +
                $"{EmojiHandler.GetEmoji(RarityTierEnum.Rare.ToString().ToLower())} Rare, " +
                $"{EmojiHandler.GetEmoji(RarityTierEnum.Epic.ToString().ToLower())} Epic, " +
                $"{EmojiHandler.GetEmoji(RarityTierEnum.Legendary.ToString().ToLower())} Legendary, " +
                $"{EmojiHandler.GetEmoji(RarityTierEnum.Mythic.ToString().ToLower())} Mythic." +
                $"\nDMG Types: {EmojiHandler.GetEmoji(DamageTypeEnum.Melee.ToString().ToLower())} Melee, " +
                $"{EmojiHandler.GetEmoji(DamageTypeEnum.Ranged.ToString().ToLower())} Ranged. " +
                $"\nAscend Tiers: {UtilityFunctions.GetTierStars(5)}" +
                $"\n" +
                $"\n**Companion attributes**:" +
                $"\n**DPS**: The damage per second a companion does." +
                $"\n**HP**: The hit points a companion has. When this reaches 0, the companion is defeated." +
                $"\n**Armor**: The damage reduction a companion has. Armor maxes out at 900, with 90% damage reduction. Each 10 points of Armor is 1% damage reduction." +
                $"\n**Accuracy**: The chance to deal critical damage (x3 DPS) a companion has. Accuracy maxes at 9,000 for 90% critical chance. Each 100 points are 1% critical chance." +
                $"\n**Agility**: The chance a companion has to dodge an attack. Agility maxes at 9,000 for 90% dodge chance. Each 100 points are 1% dodge chance." +
                $"\n" +
                $"\n**Companion classes:**" +
                $"\n**Warrior** {EmojiHandler.GetEmoji(CompanionClassesEnum.Warrior.ToString().ToLower())}: A melee specialist that is very good at close range battle." +
                $"\n**Tank** {EmojiHandler.GetEmoji(CompanionClassesEnum.Tank.ToString().ToLower())}: Has high HP and high Armor to survive as much as possible." +
                $"\n**Ranger** {EmojiHandler.GetEmoji(CompanionClassesEnum.Ranger.ToString().ToLower())}: Rangers are very fragile but very strong. They deal True damage. Which means they ignore all enemy armor." +
                $"\n**Assassin** {EmojiHandler.GetEmoji(CompanionClassesEnum.Assassin.ToString().ToLower())}: Assassins are very good at taking out the backline. Assassins always hit the enemies that are in the back line first." +
                $"\n" +
                $"\n**Companion elements:**" +
                $"\n**Nature** {EmojiHandler.GetEmoji(ElementTypeEnum.Nature.ToString().ToLower())}: + 50% DPS on Water / -50% DPS on Fire. " +
                $"\n**Water** {EmojiHandler.GetEmoji(ElementTypeEnum.Water.ToString().ToLower())}: + 50% DPS on Fire / -50% DPS on Nature. " +
                $"\n**Fire** {EmojiHandler.GetEmoji(ElementTypeEnum.Fire.ToString().ToLower())}: + 50% DPS on Nature / -50% DPS on Water. ",
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
