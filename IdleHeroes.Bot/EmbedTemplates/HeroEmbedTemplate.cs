using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Models;
using IdleHeroes.Support;
using IdleHeroesDAL.Models;
using System;
using static IdleHeroes.Support.ProfileHelper;
using IdleHeroesDAL.Enums;

namespace IdleHeroes.EmbedTemplates
{
    public static class HeroEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed = null;

        public static DiscordEmbedBuilder Get(CommandContext ctx, Profile profile)
        {
            ProfileLevelData profileLevelData = CalculateProfileData(profile);

            _embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Turquoise,
                //Title = $"{profile.Username}'s profile",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"{profile.Username}'s Hero ({profile.DiscordName})",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"{EmojiHandler.GetEmoji("sp")} {profileLevelData.Level - profile.SkillPointsSpent - 1}" +
                $" • {EmojiHandler.GetEmoji("gem")} {UtilityFunctions.FormatNumber(profile.Gems)}" +
                $"\n" +
                $"\nUse `.skills` to view and update your Hero skills.",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail()
                {
                    Url = ctx.Message.Author.AvatarUrl
                },
                //ImageUrl = ctx.Message.Author.AvatarUrl,
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            _embed.AddField("Level", 
                $"{EmojiHandler.GetEmoji("lvl")} {profileLevelData.Level}" +
                $"\n{EmojiHandler.GetEmoji("xp")} {profileLevelData.AvailableXP}" +
                $"\nLevel up: {profileLevelData.NextLevelXPCost}", true);

            _embed.AddField("Attributes -> Next Lv.", $"DPS: {CalculateAttributeString(profile, CompanionAttributeEnum.DPS)}" +
                    $" -> {CalculateAttributeString(profile, CompanionAttributeEnum.DPS, true)}" +
                    $"\nHP: {CalculateAttributeString(profile, CompanionAttributeEnum.HP)}" +
                    $" -> {CalculateAttributeString(profile, CompanionAttributeEnum.HP, true)}" +
                    $"\nArmor: {CalculateAttributeString(profile, CompanionAttributeEnum.Armor)}" +
                    $" -> {CalculateAttributeString(profile, CompanionAttributeEnum.Armor, true)}" +
                    $"\nAccuracy: {CalculateAttributeString(profile, CompanionAttributeEnum.Accuracy)}" +
                    $" -> {CalculateAttributeString(profile, CompanionAttributeEnum.Accuracy, true)}" +
                    $"\nAgility: {CalculateAttributeString(profile, CompanionAttributeEnum.Agility)}" +
                    $" -> {CalculateAttributeString(profile, CompanionAttributeEnum.Agility, true)}" +
                $"\n", true);

            return _embed;
        }
    }
}
