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
    public class StageCommands : BaseCommandModule
    {
        IProfileService _profileService = null;
        IStageService _stageService = null;

        public StageCommands(IProfileService profileService, IStageService stageService)
        {
            _profileService = profileService;
            _stageService = stageService;
        }

        [Command("stage")]
        [Description("Check the stage you currently are in.")]
        public async Task Stage(CommandContext ctx)
        {
            try
            {
                Profile profile = await _profileService.FindByDiscordID(ctx).ConfigureAwait(false);
                Stage stage = await _stageService.GetStageFromProfile(profile).ConfigureAwait(false);

                await ctx.Channel.SendMessageAsync(embed: StageEmbedTemplate.Show(ctx, profile, stage).Build()).ConfigureAwait(false);
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
