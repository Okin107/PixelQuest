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

        public static DiscordEmbedBuilder Show(CommandContext ctx, Profile profile)
        {
            _embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Gold,
                //Title = $"{profile.Username}'s Current Stage",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"Tavern",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"{EmojiHandler.GetEmoji("food")} {UtilityFunctions.FormatNumber(profile.Food)}" +
                $" • {EmojiHandler.GetEmoji("gem")} {UtilityFunctions.FormatNumber(profile.Gems)}" +
                $"\n\nWelcome to the Tavern. Here you can meet and hire Companions to help you in your journey." +
                $"\n" +
                $"\nYou can refresh the tavern for **1** {EmojiHandler.GetEmoji("gem")} each time by using `.tavern refresh`.",
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            foreach (TavernCompanion tavernCompanion in profile.Tavern.Companions)
            {
                //Purchased or not
                string costString = $"{UtilityFunctions.FormatNumber(tavernCompanion.FoodCost)} {EmojiHandler.GetEmoji("food")}";
                string nameString = tavernCompanion.Companion.Name;
                TavernPurchase alreadyPurchasedCompanion = profile.Tavern.Purchases.Find(x => x.TavernCompanion.Id == tavernCompanion.Id && x.PurchaseDate.Month == DateTime.Now.Month && x.PurchaseDate.Day == DateTime.Now.Day);

                if (alreadyPurchasedCompanion != null)
                {
                    costString = "Purchased";
                    nameString = $"~~{tavernCompanion.Companion.Name}~~";;
                }

                _embed.AddField($"**{tavernCompanion.Companion.Id}**: {EmojiHandler.GetEmoji(tavernCompanion.Companion.IconName)} {nameString} " +
                $"{EmojiHandler.GetEmoji(tavernCompanion.Companion.RarityTier.ToString().ToLower())}",
                $"\n" +
                $"\n{EmojiHandler.GetEmoji(tavernCompanion.Companion.Element.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(tavernCompanion.Companion.Class.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(tavernCompanion.Companion.DamageType.ToString().ToLower())} " +
                $"\n" +
                $"\n**Attributes**" +
                $"\nTier: {UtilityFunctions.GetTierStars(1)}" +
                $"\nDPS: {UtilityFunctions.FormatNumber(tavernCompanion.Companion.DPS)}" +
                $"\nHP: {UtilityFunctions.FormatNumber(tavernCompanion.Companion.HP)}" +
                $"\nArmor: {UtilityFunctions.FormatNumber(tavernCompanion.Companion.Armor)}" +
                $"\nAccuracy: {UtilityFunctions.FormatNumber(tavernCompanion.Companion.Accuracy)}" +
                $"\nAgility: {UtilityFunctions.FormatNumber(tavernCompanion.Companion.Agility)}" +
                 $"\n" +
                 $"\nCost: {costString}", true);
            }

            return _embed;
        }
    }
}
