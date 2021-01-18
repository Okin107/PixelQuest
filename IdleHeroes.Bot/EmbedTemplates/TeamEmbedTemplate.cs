﻿using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Support;
using IdleHeroesDAL.Enums;
using IdleHeroesDAL.Models;
using System;
using System.Collections.Generic;

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

            _embed.AddField($"Team grid", $"🟦 \u200B \u200B \u200B \u200B \u200B 🇨 " +
                $"\u200B \u200B \u200B \u200B \u200B 🇧 " +
                $"\u200B \u200B \u200B \u200B \u200B 🇦 \n" +
                                        $"\n1️⃣ \u200B \u200B \u200B \u200B \u200B {stringC1} \u200B \u200B \u200B \u200B \u200B {stringB1} \u200B \u200B \u200B \u200B \u200B {stringA1}" +
                                        $"\n" +
                                        $"\n2️⃣ \u200B \u200B \u200B \u200B \u200B {stringC2} \u200B \u200B \u200B \u200B \u200B {stringB2} \u200B \u200B \u200B \u200B \u200B {stringA2}" +
                                        $"\n" +
                                        $"\n3️⃣ \u200B \u200B \u200B \u200B \u200B {stringC3} \u200B \u200B \u200B \u200B \u200B {stringB3} \u200B \u200B \u200B \u200B \u200B {stringA3}", true);

            return _embed;
        }
    }
}
