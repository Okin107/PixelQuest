using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroesDAL.Models;
using System;

namespace IdleHeroes.EmbedTemplates
{
    public static class WarningEmbedTemplate
    {
        private static DiscordEmbedBuilder Embed = null;

        public static DiscordEmbedBuilder Get(CommandContext ctx, string message)
        {
            Embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Orange,
                Description = "⚠️ **Warning**"
                +"\n"
                + $"\n {message}",
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            return Embed;
        }
    }
}
