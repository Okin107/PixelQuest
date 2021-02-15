using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Support;
using IdleHeroesDAL.Enums;
using IdleHeroesDAL.Models;
using System;
using System.Linq;

namespace IdleHeroes.EmbedTemplates
{
    public static class StageInfoEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed;

        public static DiscordEmbedBuilder Show(CommandContext ctx, Profile profile, Stage stage = null)
        {
            Stage selectedStage = profile.Stage;

            if (stage != null)
            {
                selectedStage = stage;
            }

            _embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Aquamarine,
                //Title = $"{profile.Username}'s Current Stage",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"{profile.Username} - Stage {selectedStage.Number} - Retries: {profile.BattleRetries}",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"Use `.farm` to preview your idle resources." +
                $"\nUse `.team` to see and manage your active team." +
                $"\nUse `.stage fight` to initate a battle on this stage." +
                $"\nUse `.stage <stageNunmber` to preview a previous stage or `.stage <stageNumber> fight` to fight a previous stage again." +
                $"\n" +
                $"\nDifficulty: {selectedStage.Difficulty}" +
                $"\n{EmojiHandler.GetEmoji("xp")} {UtilityFunctions.FormatNumber(selectedStage.XPPerMinute)} per min" +
                $"\n{EmojiHandler.GetEmoji("coin")} {UtilityFunctions.FormatNumber(selectedStage.CoinsPerMinute)} per min" +
                $"\n{EmojiHandler.GetEmoji("food")} {selectedStage.FoodChancePerMinute}% for {UtilityFunctions.FormatNumber(selectedStage.FoodAmount)} per min" +
                $"\n{EmojiHandler.GetEmoji("gem")} {selectedStage.GemsDropChancePerMinute}% for {UtilityFunctions.FormatNumber(selectedStage.GemsAmount)} per min" +
                $"\n{EmojiHandler.GetEmoji("relic")} {selectedStage.RelicsDropChancePerMinute}% for {UtilityFunctions.FormatNumber(selectedStage.RelicsAmount)} per min" +
                $"\nBattle time: {selectedStage.TimeToBeat}"
                ,
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail()
                {
                    Url = ctx.Message.Author.AvatarUrl
                },
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            string stringA1 = "🔳";
            string stringA2 = "🔳";
            string stringA3 = "🔳";
            string stringB1 = "🔳";
            string stringB2 = "🔳";
            string stringB3 = "🔳";
            string stringC1 = "🔳";
            string stringC2 = "🔳";
            string stringC3 = "🔳";

            //Find the team
            #region teamStrings
            StageEnemy stageEnemy = selectedStage.Enemies.Find(x => x.Position == TeamPositionEnum.A1);
            if (stageEnemy != null)
            {
                stringA1 = stringA1 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringA1;
            }

            stageEnemy = selectedStage.Enemies.Find(x => x.Position == TeamPositionEnum.A2);
            if (stageEnemy != null)
            {
                stringA2 = stringA2 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringA2;
            }

            stageEnemy = selectedStage.Enemies.Find(x => x.Position == TeamPositionEnum.A3);
            if (stageEnemy != null)
            {
                stringA3 = stringA3 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringA3;
            }

            stageEnemy = selectedStage.Enemies.Find(x => x.Position == TeamPositionEnum.B1);
            if (stageEnemy != null)
            {
                stringB1 = stringB1 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringB1;
            }

            stageEnemy = selectedStage.Enemies.Find(x => x.Position == TeamPositionEnum.B2);
            if (stageEnemy != null)
            {
                stringB2 = stringB2 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringB2;
            }

            stageEnemy = selectedStage.Enemies.Find(x => x.Position == TeamPositionEnum.B3);
            if (stageEnemy != null)
            {
                stringB3 = stringB3 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringB3;
            }

            stageEnemy = selectedStage.Enemies.Find(x => x.Position == TeamPositionEnum.C1);
            if (stageEnemy != null)
            {
                stringC1 = stringC1 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringC1;
            }

            stageEnemy = selectedStage.Enemies.Find(x => x.Position == TeamPositionEnum.C2);
            if (stageEnemy != null)
            {
                stringC2 = stringC2 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringC2;
            }

            stageEnemy = selectedStage.Enemies.Find(x => x.Position == TeamPositionEnum.C3);
            if (stageEnemy != null)
            {
                stringC3 = stringC3 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringC3;
            }
            #endregion

            _embed.AddField($"**Enemy positions**", $"🇦 \u200B \u200B \u200B \u200B \u200B " +
                $" 🇧 \u200B \u200B \u200B \u200B \u200B " +
                $" 🇨 \u200B \u200B \u200B \u200B \u200B {EmojiHandler.GetEmoji("blank")} \n" +
                $"\n{stringA1} \u200B \u200B \u200B \u200B \u200B {stringB1} \u200B \u200B \u200B \u200B \u200B {stringC1} \u200B \u200B \u200B \u200B \u200B 1️⃣" +
                $"\n" +
                $"\n{stringA2} \u200B \u200B \u200B \u200B \u200B {stringB2} \u200B \u200B \u200B \u200B \u200B {stringC2} \u200B \u200B \u200B \u200B \u200B 2️⃣" +
                $"\n" +
                $"\n{stringA3} \u200B \u200B \u200B \u200B \u200B {stringB3} \u200B \u200B \u200B \u200B \u200B {stringC3} \u200B \u200B \u200B \u200B \u200B 3️⃣", true);

            selectedStage.Enemies = selectedStage.Enemies.OrderBy(x => x.Position).ToList();

            string companionString = "";
            string chanceToGetCompanion = "(100%)";

            if (selectedStage.Number < profile.Stage.Number)
            {
                chanceToGetCompanion = $"({selectedStage.ChanceToGetCompanion}%)";
            }

            if (selectedStage.Companion != null)
            {
                companionString = $"{EmojiHandler.GetEmoji(selectedStage.Companion.IconName)} " +
                $"{selectedStage.Companion.Id}: {selectedStage.Companion.Name} {chanceToGetCompanion}" +
                $"\n{EmojiHandler.GetEmoji(selectedStage.Companion.Element.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(selectedStage.Companion.Class.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(selectedStage.Companion.DamageType.ToString().ToLower())}" +
                $"{EmojiHandler.GetEmoji(selectedStage.Companion.RarityTier.ToString().ToLower())}";
            }



            _embed.AddField("**Fight rewards**",
                $"{companionString}" +
                $"\n{EmojiHandler.GetEmoji("xp")} {UtilityFunctions.FormatNumber(selectedStage.StaticXP)}" +
                $"\n{EmojiHandler.GetEmoji("coin")} {UtilityFunctions.FormatNumber(selectedStage.StaticCoins)}" +
                $"\n{EmojiHandler.GetEmoji("food")} {UtilityFunctions.FormatNumber(selectedStage.StaticFood)}" +
                $"\n{EmojiHandler.GetEmoji("gem")} {UtilityFunctions.FormatNumber(selectedStage.StaticGems)}" +
                $"\n{EmojiHandler.GetEmoji("relic")} {UtilityFunctions.FormatNumber(selectedStage.StaticRelics)}", true);

            _embed.AddField($"\u200B", "**Enemy details**");

            foreach (StageEnemy enemy in selectedStage.Enemies)
            {
                _embed.AddField($"{EmojiHandler.GetEmoji(enemy.IconName)}" +
                $"**{enemy.Position}: {enemy.Name}**",
                $"\n{EmojiHandler.GetEmoji(enemy.Companion.Element.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(enemy.Companion.Class.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(enemy.Companion.DamageType.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(enemy.Companion.RarityTier.ToString().ToLower())}" +
                $"\n{UtilityFunctions.GetTierStars((int)enemy.RarirtyTier)}" +
                $"\nLv: {enemy.Level}" +
                $"\nDPS: {CompanionHelper.CalculateAttributeString(enemy, CompanionAttributeEnum.DPS)}" +
                $"\nHP: {CompanionHelper.CalculateAttributeString(enemy, CompanionAttributeEnum.HP)}" +
                $"\nArmor: {CompanionHelper.CalculateAttributeString(enemy, CompanionAttributeEnum.Armor)}" +
                $"\nAcc: {CompanionHelper.CalculateAttributeString(enemy, CompanionAttributeEnum.Accuracy)}" +
                $"\nAgi: {CompanionHelper.CalculateAttributeString(enemy, CompanionAttributeEnum.Agility)}", true);
            }

            return _embed;
        }
    }
}
