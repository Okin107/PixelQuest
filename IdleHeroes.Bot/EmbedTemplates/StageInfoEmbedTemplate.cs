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

        public static DiscordEmbedBuilder Show(CommandContext ctx, Profile profile)
        {
            _embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Aquamarine,
                //Title = $"{profile.Username}'s Current Stage",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"{profile.Username} - Stage {profile.Stage.Number}",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"**General info**" +
                $"\nDifficulty: {profile.Stage.Difficulty}" +
                $"\n{EmojiHandler.GetEmoji("xp")} {UtilityFunctions.FormatNumber(profile.Stage.XPPerMinute)} per min" +
                $"\n{EmojiHandler.GetEmoji("coin")} {UtilityFunctions.FormatNumber(profile.Stage.CoinsPerMinute)} per min" +
                $"\n{EmojiHandler.GetEmoji("food")} {profile.Stage.FoodChancePerMinute}% for {UtilityFunctions.FormatNumber(profile.Stage.FoodAmount)} per min" +
                $"\n{EmojiHandler.GetEmoji("gem")} {profile.Stage.GemsDropChancePerMinute}% for {UtilityFunctions.FormatNumber(profile.Stage.GemsAmount)} per min" +
                $"\n{EmojiHandler.GetEmoji("relic")} {profile.Stage.RelicsDropChancePerMinute}% for {UtilityFunctions.FormatNumber(profile.Stage.RelicsAmount)} per min" +
                $"\nTime to beat: {profile.Stage.TimeToBeat}"
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
            StageEnemy stageEnemy = profile.Stage.Enemies.Find(x => x.Position == TeamPositionEnum.A1);
            if (stageEnemy != null)
            {
                stringA1 = stringA1 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringA1;
            }

            stageEnemy = profile.Stage.Enemies.Find(x => x.Position == TeamPositionEnum.A2);
            if (stageEnemy != null)
            {
                stringA2 = stringA2 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringA2;
            }

            stageEnemy = profile.Stage.Enemies.Find(x => x.Position == TeamPositionEnum.A3);
            if (stageEnemy != null)
            {
                stringA3 = stringA3 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringA3;
            }

            stageEnemy = profile.Stage.Enemies.Find(x => x.Position == TeamPositionEnum.B1);
            if (stageEnemy != null)
            {
                stringB1 = stringB1 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringB1;
            }

            stageEnemy = profile.Stage.Enemies.Find(x => x.Position == TeamPositionEnum.B2);
            if (stageEnemy != null)
            {
                stringB2 = stringB2 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringB2;
            }

            stageEnemy = profile.Stage.Enemies.Find(x => x.Position == TeamPositionEnum.B3);
            if (stageEnemy != null)
            {
                stringB3 = stringB3 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringB3;
            }

            stageEnemy = profile.Stage.Enemies.Find(x => x.Position == TeamPositionEnum.C1);
            if (stageEnemy != null)
            {
                stringC1 = stringC1 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringC1;
            }

            stageEnemy = profile.Stage.Enemies.Find(x => x.Position == TeamPositionEnum.C2);
            if (stageEnemy != null)
            {
                stringC2 = stringC2 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringC2;
            }

            stageEnemy = profile.Stage.Enemies.Find(x => x.Position == TeamPositionEnum.C3);
            if (stageEnemy != null)
            {
                stringC3 = stringC3 == "🔳"
                ? $"{EmojiHandler.GetEmoji(stageEnemy.IconName)}"
                : stringC3;
            }
            #endregion

            _embed.AddField($"Enemy grid", $"🇦 \u200B \u200B \u200B \u200B \u200B " +
                $" 🇧 \u200B \u200B \u200B \u200B \u200B " +
                $" 🇨 \u200B \u200B \u200B \u200B \u200B {EmojiHandler.GetEmoji("blank")} \n" +
                $"\n{stringA1} \u200B \u200B \u200B \u200B \u200B {stringB1} \u200B \u200B \u200B \u200B \u200B {stringC1} \u200B \u200B \u200B \u200B \u200B 1️⃣" +
                $"\n" +
                $"\n{stringA2} \u200B \u200B \u200B \u200B \u200B {stringB2} \u200B \u200B \u200B \u200B \u200B {stringC2} \u200B \u200B \u200B \u200B \u200B 2️⃣" +
                $"\n" +
                $"\n{stringA3} \u200B \u200B \u200B \u200B \u200B {stringB3} \u200B \u200B \u200B \u200B \u200B {stringC3} \u200B \u200B \u200B \u200B \u200B 3️⃣", true);

            profile.Stage.Enemies = profile.Stage.Enemies.OrderBy(x => x.Position).ToList();

            string companionString = "";

            if(profile.Stage.Companion != null)
            {
                companionString = $"**{profile.Stage.Companion.Id}**: {EmojiHandler.GetEmoji(profile.Stage.Companion.IconName)} {profile.Stage.Companion.Name}" +
                $"\n{EmojiHandler.GetEmoji(profile.Stage.Companion.Element.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(profile.Stage.Companion.Class.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(profile.Stage.Companion.DamageType.ToString().ToLower())}";
            }

            _embed.AddField("Fight rewards",
                $"{EmojiHandler.GetEmoji("xp")} {UtilityFunctions.FormatNumber(profile.Stage.StaticXP)}" +
                $"\n{EmojiHandler.GetEmoji("coin")} {UtilityFunctions.FormatNumber(profile.Stage.StaticCoins)}" +
                $"\n{EmojiHandler.GetEmoji("food")} {UtilityFunctions.FormatNumber(profile.Stage.StaticFood)}" +
                $"\n{EmojiHandler.GetEmoji("gem")} {UtilityFunctions.FormatNumber(profile.Stage.StaticGems)}" +
                $"\n{EmojiHandler.GetEmoji("relic")} {UtilityFunctions.FormatNumber(profile.Stage.StaticRelics)}" +
                $"\n{companionString}", true);

            _embed.AddField($"\u200B", "**Grid details**");

            foreach (StageEnemy enemy in profile.Stage.Enemies)
            {
                _embed.AddField($"**{enemy.Position}**: {EmojiHandler.GetEmoji(enemy.IconName)} {enemy.Name}",
                $"\n{EmojiHandler.GetEmoji(enemy.Companion.Element.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(enemy.Companion.Class.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(enemy.Companion.DamageType.ToString().ToLower())} " +
                $"\nLv: {enemy.Level}" +
                $"\nDPS: {CompanionHelper.CalculateAttribute(enemy, CompanionAttributeEnum.DPS)}" +
                $"\nHP: {CompanionHelper.CalculateAttribute(enemy, CompanionAttributeEnum.HP)}", true);
            }

            return _embed;
        }
    }
}
