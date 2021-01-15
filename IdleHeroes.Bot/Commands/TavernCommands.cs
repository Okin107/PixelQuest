using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using IdleHeroes.EmbedTemplates;
using IdleHeroes.Models;
using IdleHeroes.Services;
using IdleHeroes.Support;
using IdleHeroesDAL;
using IdleHeroesDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdleHeroes.Commands
{
    public class TavernCommands : BaseCommandModule
    {
        ITavernService _tavernService = null;
        IProfileService _profileService = null;
        ICompanionService _companionService = null;
        private readonly DatabaseContext _context;

        public TavernCommands(DatabaseContext context, ITavernService tavernService, IProfileService profileService, ICompanionService companionService)
        {
            _context = context;
            _tavernService = tavernService;
            _profileService = profileService;
            _companionService = companionService;
        }

        [Command("tavern")]
        [Description("Here you can meet and hire Companions to help you in your journey.")]
        public async Task Tavern(CommandContext ctx, [Description("The ID of the Companion that you want to purchase. This is the number in front of the name of each Companion.")] string companionId = null)
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

                //Check if tavern has to refresh


                //Buy companion
                if (!string.IsNullOrEmpty(companionId))
                {
                    try
                    {
                        int compId = Convert.ToInt32(companionId);

                        await PurchaseCompanion(ctx, compId);
                        return;
                    }
                    catch (Exception ex)
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"The companion ID is wrong. Use `.help tavern` to find out more.").Build())
                .ConfigureAwait(false);
                        return;
                    }


                }

                Tavern tavern = await _tavernService.Get(ctx);

                await ctx.Channel.SendMessageAsync(embed: TavernEmbedTemplate.Show(ctx, tavern).Build())
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

        private async Task PurchaseCompanion(CommandContext ctx, int compId)
        {
            Tavern tavern = await _tavernService.Get(ctx);
            Profile profile = await _profileService.FindByDiscordID(ctx).ConfigureAwait(false);
            List<Companion> companions = await _companionService.GetCompanions();

            TavernCompanion selectedCompanion = tavern.Companions.Find(x => x.Id == compId);

            if(selectedCompanion == null)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"The companion ID is wrong. Use `.help tavern` to find out more.").Build())
                .ConfigureAwait(false);
            }

            //Check and remove the resources from profile
            if(selectedCompanion.FoodCost > profile.Food)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You only have {profile.Food} {UtilityFunctions.GetEmoji(ctx, "bot_food")}," +
                    $" but you need {selectedCompanion.FoodCost} {UtilityFunctions.GetEmoji(ctx, "bot_food")} to hire this Companion.").Build())
                .ConfigureAwait(false);
                return;
            }

            //Find if already owned
            OwnedCompanions ownedCompanionSearch = profile.OwnedCompanions.Find(x => x.Companion.Id == selectedCompanion.Companion.Id);

            OwnedCompanions purchasedCompanion = null;
            if(ownedCompanionSearch == null)
            {
                purchasedCompanion = new OwnedCompanions()
                {
                    Companion = selectedCompanion.Companion,
                    CompanionCopies = 1,
                    CompanionLevel = 1
                };

                profile.OwnedCompanions.Add(purchasedCompanion);
            }
            else
            {
                ownedCompanionSearch.CompanionCopies += 1;
            }

            profile.Food -= selectedCompanion.FoodCost;

            await _profileService.Update(ctx, profile);

            await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"Successfully purchased " +
                $"**{UtilityFunctions.GetEmoji(ctx, purchasedCompanion.Companion.IconName)}" +
                $" {purchasedCompanion.Companion.Name}**.").Build())
                   .ConfigureAwait(false);
        }
    }
}
