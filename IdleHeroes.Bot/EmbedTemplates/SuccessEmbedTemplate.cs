using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroesDAL.Models;
using System;

namespace IdleHeroes.EmbedTemplates
{
    public static class SuccessEmbedTemplate
    {
        private static DiscordEmbedBuilder Embed = null;

        public static DiscordEmbedBuilder Get(CommandContext ctx, string message)
        {
            Embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Green,
                Description = "✅ **Success**"
                + "\n"
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
