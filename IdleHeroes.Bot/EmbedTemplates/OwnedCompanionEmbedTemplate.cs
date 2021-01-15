using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Support;
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
                Description = $"This is your Companions page. Here you can manage all of your Companions.",
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            foreach (OwnedCompanions ownedCompanion in profile.OwnedCompanions)
            {
                double levelMultiplierBoost = 1;

                if (ownedCompanion.CompanionLevel >= ownedCompanion.Companion.LevelToMultiplyIncreases)
                {
                    levelMultiplierBoost = Math.Floor((ownedCompanion.CompanionLevel / ownedCompanion.Companion.LevelToMultiplyIncreases) * 2);
                }

                _embed.AddField($"**{ownedCompanion.Companion.Id}**: {UtilityFunctions.GetEmoji(ctx, ownedCompanion.Companion.IconName)} {ownedCompanion.Companion.Name}" +
                    $" (Lv: {ownedCompanion.CompanionLevel})",
                $"\n{ownedCompanion.Companion.Lore}" +
                $"\n" +
                $"\n**General**" +
                $"\nElement: {ownedCompanion.Companion.Element} " +
                $"\nClass: {ownedCompanion.Companion.Class} " +
                $"\nDMG Type: {ownedCompanion.Companion.DamageType} " +
                $"\n" +
                $"\n**Attributes**" +
                $"\nDPS: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.DPS * (ulong)Math.Pow(ownedCompanion.Companion.DPSIncreasePerLevel, ownedCompanion.CompanionLevel) * (ulong)levelMultiplierBoost)}" +
                $"\nHP: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.HP * (ulong)Math.Pow(ownedCompanion.Companion.HPIncreasePerLevel, ownedCompanion.CompanionLevel) * (ulong)levelMultiplierBoost)}" +
                $"\nArmor: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Armor * (ulong)Math.Pow(ownedCompanion.Companion.ArmorIncreasePerLevel, ownedCompanion.CompanionLevel) * (ulong)levelMultiplierBoost)}" +
                $"\nAccuracy: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Accuracy * (ulong)Math.Pow(ownedCompanion.Companion.AccuracyIncreasePerLevel, ownedCompanion.CompanionLevel) * (ulong)levelMultiplierBoost)}" +
                $"\nAgility: {UtilityFunctions.FormatNumber(ownedCompanion.Companion.Agility * (ulong)Math.Pow(ownedCompanion.Companion.AgilityIncreasePerLevel, ownedCompanion.CompanionLevel) * (ulong)levelMultiplierBoost)}", true);
            }

            return _embed;
        }
    }
}
