using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using IdleHeroes.Support;
using IdleHeroesDAL.Enums;
using IdleHeroesDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdleHeroes.EmbedTemplates
{
    public static class CodexEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed;

        public static List<Page> Show(CommandContext ctx, List<Companion> companions)
        {
            List<Page> pages = new List<Page>();

            _embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Brown,
                //Title = $"{profile.Username}'s Current Stage",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"Companions codex",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"Here you can preview all the companions at their maximum level." +
                $"\n" +
                $"\nTo preview higher companion rarities use `.codex <rarity>`. Rarity names can be seen on `.codex stats`.",
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            List<Companion> orderedCompanions = companions.OrderBy(x => x.Id).ToList();

            int i = 1;
            foreach (Companion companion in orderedCompanions)
            {
                _embed.AddField($"{EmojiHandler.GetEmoji(companion.IconName)} " +
                $"**{companion.Id}: {companion.Name}**",
                $"\n{EmojiHandler.GetEmoji(companion.Element.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(companion.Class.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(companion.DamageType.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(companion.RarityTier.ToString().ToLower())}" +
                $"\n{UtilityFunctions.GetTierStars(5)}" +
                $"\nLv: {companion.MaxLevel}" +
                $"\nDPS: {CompanionHelper.CalculateAttributeString(companion, CompanionAttributeEnum.DPS)}" +
                $"\nHP: {CompanionHelper.CalculateAttributeString(companion, CompanionAttributeEnum.HP)}" +
                $"\nArmor: {CompanionHelper.CalculateAttributeString(companion, CompanionAttributeEnum.Armor)}" +
                $"\nAccuracy: {CompanionHelper.CalculateAttributeString(companion, CompanionAttributeEnum.Accuracy)}" +
                $"\nAgility: {CompanionHelper.CalculateAttributeString(companion, CompanionAttributeEnum.Agility)}" +
                $"\n", true);

                if (i % 6 == 0)
                {
                    Page page = new Page()
                    {
                        Embed = _embed
                    };
                    pages.Add(page);
                    _embed.ClearFields();
                    i = 1;
                } else
                {
                    i++;
                }
            }

            if (i % 6 == 0)
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
