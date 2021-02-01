using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroesDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IdleHeroes.EmbedTemplates
{
    public static class LeaderboardEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed = null;

        public static DiscordEmbedBuilder Show(CommandContext ctx, Profile myProfile, List<Profile> allProfiles)
        {
            _embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Turquoise,
                //Title = $"{profile.Username}'s profile",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"Leaderboard (Stages)",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                //Description = $"**Discord Name**: ",
                //ImageUrl = ctx.Message.Author.AvatarUrl,
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            allProfiles = allProfiles.OrderByDescending(x => x.Stage.Number).ToList();
            allProfiles = allProfiles.Take(20).ToList();

            string descriptionString = "";
            int i = 1;

            foreach(Profile profile in allProfiles)
            {
                //It's me
                if(myProfile.Id == profile.Id)
                {
                    descriptionString += $"\n**{i}: {profile.Username} - Stage: {profile.Stage.Number}**";
                }
                else
                {
                    descriptionString += $"\n{i}: {profile.Username} - Stage: {profile.Stage.Number}";
                }

                i++;
            }

            _embed.Description = descriptionString;

            return _embed;
        }
    }
}
