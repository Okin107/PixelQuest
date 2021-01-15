using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using IdleHeroes.EmbedTemplates;
using IdleHeroes.Models;
using IdleHeroes.Services;
using IdleHeroesDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdleHeroes.Commands
{
    public class CompanionCommands : BaseCommandModule
    {
        ICompanionService _companionService = null;
        IProfileService _profileService = null;

        public CompanionCommands(ICompanionService companionService, IProfileService profileService)
        {
            _companionService = companionService;
            _profileService = profileService;
        }

        [Command("codex")]
        [Description("Preview all the companions at their maximum level and stats.")]
        public async Task CompanionCodex(CommandContext ctx)
        {
            try
            {
                List<Companion> companionList = await _companionService.GetCompanions();

                await ctx.Channel.SendMessageAsync(embed: CodexEmbedTemplate.Show(ctx, companionList).Build())
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

        [Command("comp")]
        [Description("Preview all the companions at their maximum level and stats.")]
        public async Task OwnedCompanions(CommandContext ctx)
        {
            try
            {
                //Check if user is registered
                if (!await _profileService.IsUserRegistered(ctx.Message.Author.Id))
                {
                    await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"Use `.create` to first create a Profile in order to play the game.").Build())
                        .ConfigureAwait(false);
                    return;
                }

                List<Companion> companionList = await _companionService.GetCompanions();

                await ctx.Channel.SendMessageAsync(embed: OwnedCompanionsEmbedTemplate.Show(ctx, await _profileService.FindByDiscordID(ctx)).Build())
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
