﻿using DSharpPlus.CommandsNext;
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
            TimeSpan timeToRefresh = DateTime.Today.AddDays(1) - DateTime.Now;
            double upgradeCost = profile.Tavern.TierBaseCost * Math.Pow(profile.Tavern.TierCostIncrease, profile.Tavern.Tier);
            upgradeCost = upgradeCost == 0 ? profile.Tavern.TierBaseCost : upgradeCost;

            string tierString = $"\nUse `.tavern upgrade` to upgarde to the next **Tier** for **{upgradeCost}** {EmojiHandler.GetEmoji("gem")}.";

            if (profile.Tavern.Tier >= profile.Tavern.MaxTier)
            {
                tierString = "";
            }

            _embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Gold,
                //Title = $"{profile.Username}'s Current Stage",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"Tavern - Tier {profile.Tavern.Tier + 1}",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"{EmojiHandler.GetEmoji("food")} {UtilityFunctions.FormatNumber(profile.Food)}" +
                $" • {EmojiHandler.GetEmoji("gem")} {UtilityFunctions.FormatNumber(profile.Gems)}" +
                $"\n\nWelcome to the Tavern. Here you can meet and hire Companions to help you in your journey." +
                $"\n" +
                $"\nThe tavern refreshes once a day for free." +
                $"\n" +
                $"\nUse `.tavern refresh`to refresh the tavern for **5** {EmojiHandler.GetEmoji("gem")}." +
                $"{tierString}" +
                $"\nUse `.tavern tiers` to check the different Tiers.",
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username + $" • Tavern refresh in {timeToRefresh.Hours}:{timeToRefresh.Minutes}:{timeToRefresh.Seconds}"
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
                    nameString = $"~~{tavernCompanion.Companion.Name}~~"; ;
                }

                _embed.AddField($"{EmojiHandler.GetEmoji(tavernCompanion.Companion.IconName)} " +
                    $"**{tavernCompanion.Companion.Id}:  {nameString}**",
                $"\n{EmojiHandler.GetEmoji(tavernCompanion.Companion.Element.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(tavernCompanion.Companion.Class.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(tavernCompanion.Companion.DamageType.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(tavernCompanion.Companion.RarityTier.ToString().ToLower())} " +
                $"\n{UtilityFunctions.GetTierStars(1)}" +
                $"\nDPS: {UtilityFunctions.FormatNumber(tavernCompanion.Companion.DPS)}" +
                $"\nHP: {UtilityFunctions.FormatNumber(tavernCompanion.Companion.HP)}" +
                $"\nArmor: {UtilityFunctions.FormatNumber(tavernCompanion.Companion.Armor)}" +
                $"\nAccuracy: {UtilityFunctions.FormatNumber(tavernCompanion.Companion.Accuracy)}" +
                $"\nAgility: {UtilityFunctions.FormatNumber(tavernCompanion.Companion.Agility)}" +
                $"\n**Cost: {costString}**", true);
            }

            return _embed;
        }
    }
}
