using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using IdleHeroes.Support;
using System;
using IdleHeroes.Models;
using IdleHeroes.EmbedTemplates;
using IdleHeroesDAL.Models;
using IdleHeroes.Services;
using System.Collections.Generic;

namespace IdleHeroes.Commands
{
    public class GeneralCommands : BaseCommandModule
    {
        IProfileService _profileService;

        public GeneralCommands(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [Command("ping")]
        [Description("Test the latency of the bot.")]
        public async Task Ping(CommandContext ctx)
        {
            try
            {
                var latency = ctx.Client.Ping;
                await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"Pong! `{latency}ms`").Build())
                    .ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                if(BotSettings.IsDebugMode)
                {
                    await ctx.Channel.SendMessageAsync(embed: ErrorEmbedTemplate.Get(ctx, $"COMMAND ERROR: {ex.Message}").Build())
                    .ConfigureAwait(false);
                }
                Console.WriteLine($"COMMAND ERROR: {ex.Message}");
            }
            
        }

        [Command("die")]
        [Description("Kills the bot.")]
        public async Task KillBot(CommandContext ctx)
        {
            try
            {
                if (UtilityFunctions.IsBotOwner(ctx.Message.Author.Id))
                {
                    await ctx.Client.DisconnectAsync();
                    return;
                }

                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, "This command is restricted to the **Owners** of the bot only!").Build())
                .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (BotSettings.IsDebugMode)
                {
                    await ctx.Channel.SendMessageAsync(embed: ErrorEmbedTemplate.Get(ctx, $"COMMAND ERROR: {ex.Message}").Build())
                    .ConfigureAwait(false);
                }
                Console.WriteLine($"COMMAND ERROR: {ex.Message}");
            }
        }

        [Command("lb")]
        [Description("Check out how you rank against other players.")]
        public async Task Leaderboard(CommandContext ctx, [Description("The filter you want to apply to the leaderboard. By default you will see the highest stage players. " +
            "\n`<level>` " +
            "\n`<comp>` " +
            "\n`<idle>`")] string filter = null)
        {
            try
            {
                Profile profile = await _profileService.FindByDiscordId(ctx);
                List<Profile> allProfiles = await _profileService.GetAll();

                await ctx.Channel.SendMessageAsync(embed: LeaderboardEmbedTemplate.Show(ctx, profile, allProfiles, filter).Build())
                .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (BotSettings.IsDebugMode)
                {
                    await ctx.Channel.SendMessageAsync(embed: ErrorEmbedTemplate.Get(ctx, $"COMMAND ERROR: {ex.Message}").Build())
                    .ConfigureAwait(false);
                }
                Console.WriteLine($"COMMAND ERROR: {ex.Message}");
            }


        }

        [Command("stats")]
        [Description("Check out some bot statistics.")]
        public async Task Stats(CommandContext ctx)
        {
            try
            {
                if (UtilityFunctions.IsBotOwner(ctx.Message.Author.Id))
                {
                    List<Profile> allProfiles = await _profileService.GetAll();

                    await ctx.Channel.SendMessageAsync(embed: StatsEmbedTemplate.Show(ctx, allProfiles).Build())
                    .ConfigureAwait(false);
                    return;
                }

                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, "This command is restricted to the **Owners** of the bot only!").Build())
                .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (BotSettings.IsDebugMode)
                {
                    await ctx.Channel.SendMessageAsync(embed: ErrorEmbedTemplate.Get(ctx, $"COMMAND ERROR: {ex.Message}").Build())
                    .ConfigureAwait(false);
                }
                Console.WriteLine($"COMMAND ERROR: {ex.Message}");
            }


        }
    }
}
