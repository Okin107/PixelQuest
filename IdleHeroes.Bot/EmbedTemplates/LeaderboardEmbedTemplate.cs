using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Support;
using IdleHeroesDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IdleHeroes.EmbedTemplates
{
    public static class LeaderboardEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed = null;

        public static DiscordEmbedBuilder Show(CommandContext ctx, Profile myProfile, List<Profile> allProfiles, string filter = null)
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

            string descriptionString = "";
            int i = 1;
            switch (filter)
            {
                case "level":
                    _embed.Author.Name = $"Leaderboard (XP Levels)";
                    allProfiles = allProfiles.OrderByDescending(x => ProfileHelper.CalculateProfileData(x).Level).ToList();

                    allProfiles = allProfiles.Take(20).ToList();

                    i = 1;
                    foreach (Profile profile in allProfiles)
                    {
                        //It's me
                        if (myProfile.Id == profile.Id)
                        {
                            descriptionString += $"\n**{i}: {profile.Username} - Level: {ProfileHelper.CalculateProfileData(profile).Level}**";
                        }
                        else
                        {
                            descriptionString += $"\n{i}: {profile.Username} - Level: {ProfileHelper.CalculateProfileData(profile).Level}";
                        }

                        i++;
                    }
                    break;
                case "comp":
                case "companions":
                case "companion":
                    _embed.Author.Name = $"Leaderboard (Owned Companions)";
                    allProfiles = allProfiles.OrderByDescending(x => x.OwnedCompanions.Count).ToList();

                    allProfiles = allProfiles.Take(20).ToList();

                    i = 1;
                    foreach (Profile profile in allProfiles)
                    {
                        //It's me
                        if (myProfile.Id == profile.Id)
                        {
                            descriptionString += $"\n**{i}: {profile.Username} - Companions: {profile.OwnedCompanions.Count}**";
                        }
                        else
                        {
                            descriptionString += $"\n{i}: {profile.Username} - Companions: {profile.OwnedCompanions.Count}";
                        }

                        i++;
                    }
                    break;
                case "idle":
                    _embed.Author.Name = $"Leaderboard (Last played)";
                    allProfiles = allProfiles.OrderByDescending(x => x.LastPlayed).ToList();

                    allProfiles = allProfiles.Take(20).ToList();

                    i = 1;
                    foreach (Profile profile in allProfiles)
                    {
                        //It's me
                        if (myProfile.Id == profile.Id)
                        {
                            descriptionString += $"\n**{i}: {profile.Username}: {UtilityFunctions.GetRelativeTime(profile.LastPlayed)}**";
                        }
                        else
                        {
                            descriptionString += $"\n{i}: {profile.Username}: {UtilityFunctions.GetRelativeTime(profile.LastPlayed)}";
                        }

                        i++;
                    }
                    break;
                default:
                    allProfiles = allProfiles.OrderByDescending(x => x.Stage.Number).ToList();

                    allProfiles = allProfiles.Take(20).ToList();

                    i = 1;
                    foreach (Profile profile in allProfiles)
                    {
                        //It's me
                        if (myProfile.Id == profile.Id)
                        {
                            descriptionString += $"\n**{i}: {profile.Username} - Stage: {profile.Stage.Number}**";
                        }
                        else
                        {
                            descriptionString += $"\n{i}: {profile.Username} - Stage: {profile.Stage.Number}";
                        }

                        i++;
                    }
                    break;
            }

            _embed.Description = descriptionString;

            return _embed;
        }
    }
}
