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

        public CompanionCommands(ICompanionService companionService)
        {
            _companionService = companionService;
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
    }
}
