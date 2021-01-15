using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Support;
using IdleHeroesDAL.Models;
using System;

namespace IdleHeroes.EmbedTemplates
{
    public static class StageEmbedTemplate
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
                //Description = $"**Stage info**" +
                //$"\nDifficulty: {stage.Difficulty}" +
                //$"\n{UtilityFunctions.GetEmoji(ctx, "bot_xp")} {UtilityFunctions.FormatNumber(stage.XPPerMinute)} per min" +
                //$"\n{UtilityFunctions.GetEmoji(ctx, "bot_coin")} {UtilityFunctions.FormatNumber(stage.CoinsPerMinute)} per min" +
                //$"\n{UtilityFunctions.GetEmoji(ctx, "bot_food")} {stage.FoodPerMinute}% for 1 per min" +
                //$"\n{UtilityFunctions.GetEmoji(ctx, "bot_gem")} {stage.GemsDropChancePerMinute}% for 1 per min" +
                //$"\n{UtilityFunctions.GetEmoji(ctx, "bot_relic")} {stage.RelicsDropChancePerMinute}% for 1 per min"
                //,
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

            TimeSpan idleTime = UtilityFunctions.GetIdleDisplayTime(profile);
            
            _embed.AddField("Resources found", 
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_xp")} {UtilityFunctions.FormatNumber(profile.IdleXP)}" +
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_coin")} {UtilityFunctions.FormatNumber(profile.IdleCoins)}" +
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_food")} {UtilityFunctions.FormatNumber(profile.IdleFood)}" +
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_gem")} {UtilityFunctions.FormatNumber(profile.IdleGems)}" +
                $"\n{UtilityFunctions.GetEmoji(ctx, "bot_relic")} {UtilityFunctions.FormatNumber(profile.IdleRelics)}", true);

            _embed.AddField("Idle Time", $"{idleTime.ToString("h'h, 'm'm, 's's'")}", true);

            return _embed;
        }
    }
}
