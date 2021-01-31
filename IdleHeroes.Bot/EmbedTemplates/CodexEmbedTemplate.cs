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
                _embed.AddField($"**{companion.Id}**: {EmojiHandler.GetEmoji(companion.IconName)} {companion.Name} " +
                $"{EmojiHandler.GetEmoji(companion.RarityTier.ToString().ToLower())}",
                $"\n{EmojiHandler.GetEmoji(companion.Element.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(companion.Class.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(companion.DamageType.ToString().ToLower())} " +
                $"\nLv: {companion.MaxLevel}" +
                $"\n{companion.Lore}" +
                $"\n" +
                $"\n**Attributes**" +
                $"\nTier: {UtilityFunctions.GetTierStars(5)}" +
                $"\nDPS: {CompanionHelper.CalculateAttributeString(companion, CompanionAttributeEnum.DPS)}" +
                $"\nHP: {CompanionHelper.CalculateAttributeString(companion, CompanionAttributeEnum.HP)}" +
                $"\nArmor: {CompanionHelper.CalculateAttributeString(companion, CompanionAttributeEnum.Armor)}" +
                $"\nAccuracy: {CompanionHelper.CalculateAttributeString(companion, CompanionAttributeEnum.Accuracy)}" +
                $"\nAgility: {CompanionHelper.CalculateAttributeString(companion, CompanionAttributeEnum.Agility)}", true);
            }

            return _embed;
        }
    }
}
