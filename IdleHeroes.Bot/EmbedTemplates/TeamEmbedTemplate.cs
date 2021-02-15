using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Support;
using IdleHeroesDAL.Enums;
using IdleHeroesDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IdleHeroes.EmbedTemplates
{
    public static class TeamEmbedTemplate
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
                    Name = $"{profile.Username}'s team",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"Below you can see your team. You can add, move or remove companions from the team." +
                $"\n" +
                $"\nThe front line is line **A**, middle line is **B** and backline is **C**.",
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

            //Find hero position
            TeamPositionEnum heroPosition = profile.Team.HeroTeamPosition;

            switch (heroPosition)
            {
                case TeamPositionEnum.A1:
                    stringA1 = "⚔️";
                    break;
                case TeamPositionEnum.A2:
                    stringA2 = "⚔️";
                    break;
                case TeamPositionEnum.A3:
                    stringA3 = "⚔️";
                    break;
                case TeamPositionEnum.B1:
                    stringB1 = "⚔️";
                    break;
                case TeamPositionEnum.B2:
                    stringB2 = "⚔️";
                    break;
                case TeamPositionEnum.B3:
                    stringB3 = "⚔️";
                    break;
                case TeamPositionEnum.C1:
                    stringC1 = "⚔️";
                    break;
                case TeamPositionEnum.C2:
                    stringC2 = "⚔️";
                    break;
                case TeamPositionEnum.C3:
                    stringC3 = "⚔️";
                    break;
            }

            //Find the rest of the team
            #region teamStrings
            TeamCompanion teamCompanion = profile.Team.Companions.Find(x => x.TeamPosition == TeamPositionEnum.A1);
            if (teamCompanion != null)
            {
                stringA1 = stringA1 == "🔳"
                ? $"{EmojiHandler.GetEmoji(teamCompanion.OwnedCompanion.Companion.IconName)}"
                : stringA1;
            }

            teamCompanion = profile.Team.Companions.Find(x => x.TeamPosition == TeamPositionEnum.A2);
            if (teamCompanion != null)
            {
                stringA2 = stringA2 == "🔳"
                ? $"{EmojiHandler.GetEmoji(teamCompanion.OwnedCompanion.Companion.IconName)}"
                : stringA2;
            }

            teamCompanion = profile.Team.Companions.Find(x => x.TeamPosition == TeamPositionEnum.A3);
            if (teamCompanion != null)
            {
                stringA3 = stringA3 == "🔳"
                ? $"{EmojiHandler.GetEmoji(teamCompanion.OwnedCompanion.Companion.IconName)}"
                : stringA3;
            }

            teamCompanion = profile.Team.Companions.Find(x => x.TeamPosition == TeamPositionEnum.B1);
            if (teamCompanion != null)
            {
                stringB1 = stringB1 == "🔳"
                ? $"{EmojiHandler.GetEmoji(teamCompanion.OwnedCompanion.Companion.IconName)}"
                : stringB1;
            }

            teamCompanion = profile.Team.Companions.Find(x => x.TeamPosition == TeamPositionEnum.B2);
            if (teamCompanion != null)
            {
                stringB2 = stringB2 == "🔳"
                ? $"{EmojiHandler.GetEmoji(teamCompanion.OwnedCompanion.Companion.IconName)}"
                : stringB2;
            }

            teamCompanion = profile.Team.Companions.Find(x => x.TeamPosition == TeamPositionEnum.B3);
            if (teamCompanion != null)
            {
                stringB3 = stringB3 == "🔳"
                ? $"{EmojiHandler.GetEmoji(teamCompanion.OwnedCompanion.Companion.IconName)}"
                : stringB3;
            }

            teamCompanion = profile.Team.Companions.Find(x => x.TeamPosition == TeamPositionEnum.C1);
            if (teamCompanion != null)
            {
                stringC1 = stringC1 == "🔳"
                ? $"{EmojiHandler.GetEmoji(teamCompanion.OwnedCompanion.Companion.IconName)}"
                : stringC1;
            }

            teamCompanion = profile.Team.Companions.Find(x => x.TeamPosition == TeamPositionEnum.C2);
            if (teamCompanion != null)
            {
                stringC2 = stringC2 == "🔳"
                ? $"{EmojiHandler.GetEmoji(teamCompanion.OwnedCompanion.Companion.IconName)}"
                : stringC2;
            }

            teamCompanion = profile.Team.Companions.Find(x => x.TeamPosition == TeamPositionEnum.C3);
            if (teamCompanion != null)
            {
                stringC3 = stringC3 == "🔳"
                ? $"{EmojiHandler.GetEmoji(teamCompanion.OwnedCompanion.Companion.IconName)}"
                : stringC3;
            }
            #endregion

            _embed.AddField($"**Team positions**", $"{EmojiHandler.GetEmoji("blank")} {EmojiHandler.GetEmoji("blank")} 🇨 " +
                $"{EmojiHandler.GetEmoji("blank")} 🇧 " +
                $"{EmojiHandler.GetEmoji("blank")} 🇦 \n" +
                $"\n1️⃣ {EmojiHandler.GetEmoji("blank")} {stringC1} {EmojiHandler.GetEmoji("blank")} {stringB1} {EmojiHandler.GetEmoji("blank")} {stringA1}" +
                $"\n" +
                $"\n2️⃣ {EmojiHandler.GetEmoji("blank")} {stringC2} {EmojiHandler.GetEmoji("blank")} {stringB2} {EmojiHandler.GetEmoji("blank")} {stringA2}" +
                $"{EmojiHandler.GetEmoji("blank")} ---> Enemy" +
                $"\n" +
                $"\n3️⃣ {EmojiHandler.GetEmoji("blank")} {stringC3} {EmojiHandler.GetEmoji("blank")} {stringB3} {EmojiHandler.GetEmoji("blank")} {stringA3}");

            profile.Team.Companions = profile.Team.Companions.OrderBy(x => x.TeamPosition).ToList();

            _embed.AddField($"\u200B", "**Team details**");

            _embed.AddField($"⚔️ **{heroPosition}: {profile.Username}**",
                $"\nLv: {ProfileHelper.CalculateProfileData(profile).Level}" +
                $"\nDPS: {ProfileHelper.CalculateAttributeString(profile, CompanionAttributeEnum.DPS)}" +
                $"\nHP: {ProfileHelper.CalculateAttributeString(profile, CompanionAttributeEnum.HP)}" +
                $"\nArmor: {ProfileHelper.CalculateAttributeString(profile, CompanionAttributeEnum.Armor)}" +
                $"\nAcc: {ProfileHelper.CalculateAttributeString(profile, CompanionAttributeEnum.Accuracy)}" +
                $"\nAgi: {ProfileHelper.CalculateAttributeString(profile, CompanionAttributeEnum.Agility)}", true);

            foreach (TeamCompanion companion in profile.Team.Companions)
            {
                _embed.AddField($"{EmojiHandler.GetEmoji(companion.OwnedCompanion.Companion.IconName)} " +
                    $"**{companion.TeamPosition}: {companion.OwnedCompanion.Companion.Id} {companion.OwnedCompanion.Companion.Name}**",
                $"\n{EmojiHandler.GetEmoji(companion.OwnedCompanion.Companion.Element.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(companion.OwnedCompanion.Companion.Class.ToString().ToLower())} " +
                $"{EmojiHandler.GetEmoji(companion.OwnedCompanion.Companion.DamageType.ToString().ToLower())} " +
                $"\n{UtilityFunctions.GetTierStars((int)companion.OwnedCompanion.RarirtyTier)}" +
                $"{EmojiHandler.GetEmoji(companion.OwnedCompanion.Companion.RarityTier.ToString().ToLower())}" +
                $"\nLv: {companion.OwnedCompanion.Level}/{CompanionHelper.GetMaxLevel(companion.OwnedCompanion)}" +
                $"\nDPS: {CompanionHelper.CalculateAttributeString(companion.OwnedCompanion, CompanionAttributeEnum.DPS)}" +
                $"\nHP: {CompanionHelper.CalculateAttributeString(companion.OwnedCompanion, CompanionAttributeEnum.HP)}" +
                $"\nArmor: {CompanionHelper.CalculateAttributeString(companion.OwnedCompanion, CompanionAttributeEnum.Armor)}" +
                $"\nAcc: {CompanionHelper.CalculateAttributeString(companion.OwnedCompanion, CompanionAttributeEnum.Accuracy)}" +
                $"\nAgi: {CompanionHelper.CalculateAttributeString(companion.OwnedCompanion, CompanionAttributeEnum.Agility)}", true);
            }

            return _embed;
        }
    }
}
