﻿using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Support;
using IdleHeroesDAL.Models;
using System;

namespace IdleHeroes.EmbedTemplates
{
    public static class StageInfoEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed;

        public static DiscordEmbedBuilder Show(CommandContext ctx, Profile profile, Stage stage)
        {
            _embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Aquamarine,
                //Title = $"{profile.Username}'s Current Stage",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"{profile.Username} - Stage {stage.Number}",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"**Stage info**" +
                $"\nDifficulty: {stage.Difficulty}" +
                $"\n{EmojiHandler.GetEmoji("xp")} {UtilityFunctions.FormatNumber(stage.XPPerMinute)} per min" +
                $"\n{EmojiHandler.GetEmoji("coin")} {UtilityFunctions.FormatNumber(stage.CoinsPerMinute)} per min" +
                $"\n{EmojiHandler.GetEmoji("food")} {stage.FoodPerMinute}% for 1 per min" +
                $"\n{EmojiHandler.GetEmoji("gem")} {stage.GemsDropChancePerMinute}% for 1 per min" +
                $"\n{EmojiHandler.GetEmoji("relic")} {stage.RelicsDropChancePerMinute}% for 1 per min"
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

            return _embed;
        }
    }
}