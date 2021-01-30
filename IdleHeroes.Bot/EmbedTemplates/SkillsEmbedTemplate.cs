using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Support;
using IdleHeroesDAL.Models;
using System;
using static IdleHeroes.Support.ProfileHelper;

namespace IdleHeroes.EmbedTemplates
{
    public class SkillsEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed;

        public static DiscordEmbedBuilder Show(CommandContext ctx, Profile profile)
        {
            ProfileLevelData profileLevelData = CalculateProfileData(profile);

            _embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Gold,
                //Title = $"{profile.Username}'s Current Stage",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"{profile.Username}'s Hero skills",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"Skill Points: {profileLevelData.Level - profile.SkillPointsSpent}" +
                $"\n\nWelcome to the Skills page. Here you can upgrade your Hero's skills by spending Skill Points." +
                $"\n" +
                $"\nYou can reset your Skill Points for **50** {EmojiHandler.GetEmoji("gem")} by using `.skills reset`.",
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            _embed.AddField($"**1**: DPS -> Next Lv.",
            $"\nLevel: {profile.DPSBoostLevel} -> {profile.DPSBoostLevel + 1} (Max: {profile.BoostMaxLevel})" +
            $"\nBoost: x{UtilityFunctions.FormatNumber(Math.Pow(profile.DPSBoostLevelIncrease, profile.DPSBoostLevel))} " +
            $"-> {UtilityFunctions.FormatNumber(Math.Pow(profile.DPSBoostLevelIncrease, profile.DPSBoostLevel + 1))}" +
            $"\nCost: {Math.Round(1 * Math.Pow(profile.BoostCostIncrease, profile.DPSBoostLevel - 1), 0)} Skill Points", true);

            _embed.AddField($"**2**: HP -> Next Lv.",
            $"\nLevel: {profile.HPBoostLevel} -> {profile.HPBoostLevel + 1} (Max: {profile.BoostMaxLevel})" +
            $"\nBoost: x{UtilityFunctions.FormatNumber(Math.Pow(profile.HPBoostLevelIncrease, profile.HPBoostLevel))} " +
            $"-> {UtilityFunctions.FormatNumber(Math.Pow(profile.HPBoostLevelIncrease, profile.HPBoostLevel + 1))}" +
            $"\nCost: {Math.Round(1 * Math.Pow(profile.BoostCostIncrease, profile.HPBoostLevel - 1), 0)} Skill Points", true);

            _embed.AddField($"**3**: Armor -> Next Lv.",
            $"\nLevel: {profile.ArmorBoostLevel} -> {profile.ArmorBoostLevel + 1} (Max: {profile.BoostMaxLevel})" +
            $"\nBoost: x{UtilityFunctions.FormatNumber(Math.Pow(profile.ArmorBoostLevelIncrease, profile.ArmorBoostLevel))} " +
            $"-> {UtilityFunctions.FormatNumber(Math.Pow(profile.ArmorBoostLevelIncrease, profile.ArmorBoostLevel + 1))}" +
            $"\nCost: {Math.Round(1 * Math.Pow(profile.BoostCostIncrease, profile.ArmorBoostLevel - 1), 0)} Skill Points", true);

            _embed.AddField($"**4**: Accuracy -> Next Lv.",
            $"\nLevel: {profile.AccuracyBoostLevel} -> {profile.AccuracyBoostLevel + 1} (Max: {profile.BoostMaxLevel})" +
            $"\nBoost: x{UtilityFunctions.FormatNumber(Math.Pow(profile.AccuracyBoostLevelIncrease, profile.AccuracyBoostLevel))} " +
            $"-> {UtilityFunctions.FormatNumber(Math.Pow(profile.AccuracyBoostLevelIncrease, profile.AccuracyBoostLevel + 1))}" +
            $"\nCost: {Math.Round(1 * Math.Pow(profile.BoostCostIncrease, profile.AccuracyBoostLevel - 1), 0)} Skill Points", true);

            _embed.AddField($"**5**: Agility -> Next Lv.",
            $"\nLevel: {profile.AgilityBoostLevel} -> {profile.AgilityBoostLevel + 1} (Max: {profile.BoostMaxLevel})" +
            $"\nBoost: x{UtilityFunctions.FormatNumber(Math.Pow(profile.AgilityBoostLevelIncrease, profile.AgilityBoostLevel))} " +
            $"-> {UtilityFunctions.FormatNumber(Math.Pow(profile.AgilityBoostLevelIncrease, profile.AgilityBoostLevel + 1))}" +
            $"\nCost: {Math.Round(1 * Math.Pow(profile.BoostCostIncrease, profile.AgilityBoostLevel - 1), 0)} Skill Points", true);

            return _embed;
        }
    }
}
