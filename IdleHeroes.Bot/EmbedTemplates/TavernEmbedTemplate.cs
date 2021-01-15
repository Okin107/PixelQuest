using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Support;
using IdleHeroesDAL.Models;
using System;

namespace IdleHeroes.EmbedTemplates
{
    public class TavernEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed;

        public static DiscordEmbedBuilder Show(CommandContext ctx, Tavern tavern)
        {
            _embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Brown,
                //Title = $"{profile.Username}'s Current Stage",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"Tavern",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"Welcome to the Tavern. Here you can meet and hire Companions to help you in your journey.",
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            foreach (TavernCompanion tavernCompanion in tavern.Companions)
            {
                _embed.AddField($"**{tavernCompanion.Companion.Id}**: {UtilityFunctions.GetEmoji(ctx, tavernCompanion.Companion.IconName)} {tavernCompanion.Companion.Name}" +
                    $" (Cost: {UtilityFunctions.FormatNumber(tavernCompanion.FoodCost)} {UtilityFunctions.GetEmoji(ctx, "bot_food")})",
                $"\n{tavernCompanion.Companion.Lore}" +
                $"\n" +
                $"\n**General**" +
                $"\nElement: {tavernCompanion.Companion.Element} " +
                $"\nClass: {tavernCompanion.Companion.Class} " +
                $"\nDMG Type: {tavernCompanion.Companion.DamageType} " +
                $"\n" +
                $"\n**Attributes**" +
                $"\nDPS: {UtilityFunctions.FormatNumber(tavernCompanion.Companion.DPS)}" +
                $"\nHP: {UtilityFunctions.FormatNumber(tavernCompanion.Companion.HP)}" +
                $"\nArmor: {UtilityFunctions.FormatNumber(tavernCompanion.Companion.Armor)}" +
                $"\nAccuracy: {UtilityFunctions.FormatNumber(tavernCompanion.Companion.Accuracy)}" +
                $"\nAgility: {UtilityFunctions.FormatNumber(tavernCompanion.Companion.Agility)}", true);
            }

            return _embed;
        }
    }
}
