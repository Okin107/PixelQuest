using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroesDAL.Models;
using System;
using System.Linq;

namespace IdleHeroes.EmbedTemplates
{
    public static class ProfileEmbedTemplate
    {
        private static DiscordEmbedBuilder Embed = null;

        public static DiscordEmbedBuilder Get(CommandContext ctx, Profile profile)
        {
            Embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Turquoise,
                Title = $"{profile.Username}'s profile",
                //Author = new DiscordEmbedBuilder.EmbedAuthor()
                //{
                //    Name = profile.Username,
                //    IconUrl = ctx.Guild.Members.FirstOrDefault(x => x.Value.Id == profile.DiscordID).Value.AvatarUrl
                //},
                Description = $"**Discord Name**: {profile.DiscordName}",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail()
                {
                    Url = ctx.Message.Author.AvatarUrl
                },
                //ImageUrl = ctx.Message.Author.AvatarUrl,
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            Embed.AddField("Stage Info", $"Current: {profile.CurrentStageNumber}", true);

            Embed.AddField("Resources", 
                $"Coins: {profile.Coins}" +
                $"\nFood: {profile.Food}" +
                $"\nGems: {profile.Gems}" +
                $"\nRelics: {profile.Relics}", true);

            Embed.AddField("Level & DPS", 
                $"Level: {profile.Level}" +
                $"\nXP: {profile.XP}" +
                $"\nHero DPS: {profile.BaseDPS}", true);

            Embed.AddField("Registered", $"{profile.RegisteredOn}", true);
            Embed.AddField("Max Idle Time", $"{profile.MaximumIdleRewardHours} hours", true);
            Embed.AddField("Last Played", $"{profile.LastPlayed}", true);

            return Embed;
        }
    }
}
