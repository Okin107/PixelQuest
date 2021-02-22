using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using IdleHeroes.Support;
using IdleHeroesDAL.Enums;
using IdleHeroesDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IdleHeroes.EmbedTemplates
{
    public class OwnedCompanionsEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed;

        public static List<Page> Show(CommandContext ctx, Profile profile)
        {
            List<Page> pages = new List<Page>();

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
                $"\n\nThis is your Companions page. Here you can manage all of your Companions." +
                $"\n" +
                $"\nUse `.team` to manage your active team." +
                $"\nUse `.codex stats` to understand all the companion elements and attributes.",
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

            //Order list by ID
            List<OwnedCompanion> orderedCompanions = profile.OwnedCompanions.OrderByDescending(x => x.Companion.RarityTier).ToList();

            int i = 1;
            foreach (OwnedCompanion ownedCompanion in orderedCompanions)
            {
                if (ownedCompanion.Level < CompanionHelper.GetMaxLevel(ownedCompanion) && ownedCompanion.RarirtyTier != RarityTierEnum.Mythic)
                {
                    _embed.AddField($"{EmojiHandler.GetEmoji(ownedCompanion.Companion.IconName)} " +
                        $"**{ownedCompanion.Companion.Id}: {ownedCompanion.Companion.Name}**",
                    $"\n{EmojiHandler.GetEmoji(ownedCompanion.Companion.Element.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.Class.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.DamageType.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.RarityTier.ToString().ToLower())}" +
                    $"\n{UtilityFunctions.GetTierStars((int)ownedCompanion.RarirtyTier)}" +
                    $"\nLv: {ownedCompanion.Level}/{CompanionHelper.GetMaxLevel(ownedCompanion)}" +
                    $"\nDPS: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.DPS)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.DPS, true)}" +
                    $"\nHP: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.HP)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.HP, true)}" +
                    $"\nArmor: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Armor)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Armor, true)}" +
                    $"\nAcc: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Accuracy)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Accuracy, true)}" +
                    $"\nAgi: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Agility)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Agility, true)}" +
                    $"\n**Level up: {CompanionHelper.NextLevelCost(ownedCompanion)} {EmojiHandler.GetEmoji("coin")}**" +
                    $"\n**Ascend: {ownedCompanion.Copies}/{CompanionHelper.GetAscendCopiesNeeded(ownedCompanion)} Copies**" +
                    $"\n", true);
                }
                else if (ownedCompanion.Level < CompanionHelper.GetMaxLevel(ownedCompanion) && ownedCompanion.RarirtyTier == RarityTierEnum.Mythic)
                {
                    _embed.AddField($"{EmojiHandler.GetEmoji(ownedCompanion.Companion.IconName)} " +
                        $"**{ownedCompanion.Companion.Id}: {ownedCompanion.Companion.Name}**",
                    $"\n{EmojiHandler.GetEmoji(ownedCompanion.Companion.Element.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.Class.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.DamageType.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.RarityTier.ToString().ToLower())}" +
                    $"\n{UtilityFunctions.GetTierStars((int)ownedCompanion.RarirtyTier)}" +
                    $"\nLv: {ownedCompanion.Level}/{CompanionHelper.GetMaxLevel(ownedCompanion)}" +
                    $"\nDPS: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.DPS)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.DPS, true)}" +
                    $"\nHP: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.HP)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.HP, true)}" +
                    $"\nArmor: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Armor)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Armor, true)}" +
                    $"\nAcc: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Accuracy)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Accuracy, true)}" +
                    $"\nAgi: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Agility)}" +
                    $" -> {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Agility, true)}" +
                    $"\nLevel up: {CompanionHelper.NextLevelCost(ownedCompanion)} {EmojiHandler.GetEmoji("coin")}" +
                    $"\nAscend: MAX" +
                    $"\n", true);
                }
                else if (ownedCompanion.Level == CompanionHelper.GetMaxLevel(ownedCompanion) && ownedCompanion.RarirtyTier != RarityTierEnum.Mythic)
                {
                    _embed.AddField($"{EmojiHandler.GetEmoji(ownedCompanion.Companion.IconName)} " +
                    $"**{ownedCompanion.Companion.Id}: {ownedCompanion.Companion.Name}**",
                    $"\n{EmojiHandler.GetEmoji(ownedCompanion.Companion.Element.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.Class.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.DamageType.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.RarityTier.ToString().ToLower())}" +
                    $"\n{UtilityFunctions.GetTierStars((int)ownedCompanion.RarirtyTier)}" +
                    $"\nLv: {ownedCompanion.Level}/{CompanionHelper.GetMaxLevel(ownedCompanion)}" +
                    $"\nDPS: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.DPS)}" +
                    $" -> MAX" +
                    $"\nHP: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.HP)}" +
                    $" -> MAX" +
                    $"\nArmor: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Armor)}" +
                    $" -> MAX" +
                    $"\nAcc: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Accuracy)}" +
                    $" -> MAX" +
                     $"\nAgi: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Agility)}" +
                    $" -> MAX" +
                    $"\n**Ascend: {ownedCompanion.Copies}/{CompanionHelper.GetAscendCopiesNeeded(ownedCompanion)} Copies**" +
                    $"\n", true);
                }
                else
                {
                    _embed.AddField($"{EmojiHandler.GetEmoji(ownedCompanion.Companion.IconName)} " +
                    $"**{ownedCompanion.Companion.Id}: {ownedCompanion.Companion.Name}**",
                    $"\n{EmojiHandler.GetEmoji(ownedCompanion.Companion.Element.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.Class.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.DamageType.ToString().ToLower())} " +
                    $"{EmojiHandler.GetEmoji(ownedCompanion.Companion.RarityTier.ToString().ToLower())}" +
                    $"\n{UtilityFunctions.GetTierStars((int)ownedCompanion.RarirtyTier)}" +
                    $"\nLv: {ownedCompanion.Level}/{CompanionHelper.GetMaxLevel(ownedCompanion)}" +
                    $"\nDPS: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.DPS)}" +
                    $" -> MAX" +
                    $"\nHP: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.HP)}" +
                    $" -> MAX" +
                    $"\nArmor: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Armor)}" +
                    $" -> MAX" +
                    $"\nAcc: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Accuracy)}" +
                    $" -> MAX" +
                     $"\nAgi: {CompanionHelper.CalculateAttributeString(ownedCompanion, CompanionAttributeEnum.Agility)}" +
                    $" -> MAX" +
                    $"\nAscend: MAX" +
                    $"\n", true);
                }

                if (i % 6 == 0)
                {
                    Page page = new Page()
                    {
                        Embed = _embed
                    };
                    pages.Add(page);
                    _embed.ClearFields();
                    i = 1;
                }
                else
                {
                    i++;
                }

            }

            //add remaining to last page
            if (i >= 1)
            {
                Page page = new Page()
                {
                    Embed = _embed
                };
                pages.Add(page);
            }

            return pages;
        }
    }
}
