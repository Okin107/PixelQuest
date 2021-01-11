using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroesDAL.Models;
using System;

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
                //    Name = profile.Username
                //},
                Description = "Below are the details of your profile:",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail()
                {
                    Url = ctx.Message.Author.AvatarUrl
                },
                //ImageUrl = ctx.Message.Author.AvatarUrl,
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = profile.Username
                }
            };

            Embed.AddField("Discord Name", profile.DiscordName.ToString(), true);
            Embed.AddField("Level", profile.Level.ToString(), true);
            Embed.AddField("Coins", profile.Coins.ToString(), true);
            Embed.AddField("Gems", profile.Gems.ToString(), true);
            Embed.AddField("Relics", profile.Relics.ToString(), true);

            return Embed;
        }
    }
}
