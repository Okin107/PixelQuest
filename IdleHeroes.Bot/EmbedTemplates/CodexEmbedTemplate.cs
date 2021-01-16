using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Support;
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
                Description = $"Here you can preview all the companions at their maximum level.",
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            foreach (Companion companion in companions)
            {
                _embed.AddField($"**{companion.Id}**: {EmojiHandler.GetEmoji(companion.IconName)} {companion.Name} (Lv: {companion.MaxLevel})",
                $"\n{companion.Lore}" +
                $"\n" +
                $"\n**General**" +
                $"\nElement: {companion.Element} " +
                $"\nClass: {companion.Class} " +
                $"\nDMG Type: {companion.DamageType} " +
                $"\n" +
                $"\n**Attributes**" +
                $"\nDPS: {UtilityFunctions.FormatNumber(companion.DPS * Math.Pow(companion.DPSIncreasePerLevel, companion.MaxLevel) * (companion.MaxLevel / companion.LevelToMultiplyIncreases) * 2)}" +
                $"\nHP: {UtilityFunctions.FormatNumber(companion.HP * Math.Pow(companion.HPIncreasePerLevel, companion.MaxLevel) * (companion.MaxLevel / companion.LevelToMultiplyIncreases) * 2)}" +
                $"\nArmor: {UtilityFunctions.FormatNumber(companion.Armor * Math.Pow(companion.ArmorIncreasePerLevel, companion.MaxLevel) * (companion.MaxLevel / companion.LevelToMultiplyIncreases) * 2)}" +
                $"\nAccuracy: {UtilityFunctions.FormatNumber(companion.Accuracy * Math.Pow(companion.AccuracyIncreasePerLevel, companion.MaxLevel) * (companion.MaxLevel / companion.LevelToMultiplyIncreases) * 2)}" +
                $"\nAgility: {UtilityFunctions.FormatNumber(companion.Agility * Math.Pow(companion.AgilityIncreasePerLevel, companion.MaxLevel) * (companion.MaxLevel / companion.LevelToMultiplyIncreases) * 2)}", true);
            }

            return _embed;
        }
    }
}
