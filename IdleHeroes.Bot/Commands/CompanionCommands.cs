using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using IdleHeroes.EmbedTemplates;
using IdleHeroes.Models;
using IdleHeroes.Services;
using IdleHeroes.Support;
using IdleHeroesDAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdleHeroes.Commands
{
    public class CompanionCommands : BaseCommandModule
    {
        ICompanionService _companionService;
        IProfileService _profileService;

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
        [Description("Preview and manage all your Companions.")]
        public async Task OwnedCompanions(CommandContext ctx, [Description("The Companion ID which you wish to select.")] string companionId = null, [Description("The action you want to take on the selected Copmanion (<level>, <ascend>)")] string action = null)
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

                Profile profile = await _profileService.FindByDiscordId(ctx);
                List<Companion> companionList = await _companionService.GetCompanions();

                if (!string.IsNullOrEmpty(companionId) && !string.IsNullOrEmpty(action))
                {
                    int compId;

                    try
                    {
                        compId = Convert.ToInt32(companionId);
                    }
                    catch (Exception ex)
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"Please make sure you have used the correct command syntax. Use `.help comp` to find out more.").Build())
                .ConfigureAwait(false);
                        return;
                    }

                    if (action == "level")
                    {
                        await LevelUpCompanion(ctx, profile, compId);
                        return;
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"Please make sure you have used the correct command syntax. Use `.help comp` to find out more.").Build())
                .ConfigureAwait(false);
                        return;
                    }
                }

                await ctx.Channel.SendMessageAsync(embed: OwnedCompanionsEmbedTemplate.Show(ctx, await _profileService.FindByDiscordId(ctx)).Build())
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

        private async Task LevelUpCompanion(CommandContext ctx, Profile profile, int compId)
        {
            OwnedCompanions selectedCompanion = profile.OwnedCompanions.Find(x => x.Companion.Id == compId);

            if (selectedCompanion == null)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You do not own any Companion with ID **{compId}**. Please make sure you selected the correct **Companion ID**.").Build())
    .ConfigureAwait(false);
                return;
            }

            //Max level reached
            double maxLevel = (selectedCompanion.Companion.MaxLevel / 5) * (double)selectedCompanion.CompanionAscendTier;

            if(selectedCompanion.CompanionLevel == maxLevel)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"{EmojiHandler.GetEmoji(selectedCompanion.Companion.IconName)} **{selectedCompanion.Companion.Name}** has reached the maximum level for this **Ascend Tier**.").Build())
    .ConfigureAwait(false);
                return;
            }

            //Check if it can be purchased
            double levelCost = selectedCompanion.Companion.BaseLevelCost;

            if (selectedCompanion.CompanionLevel > 1)
            {
                double levelCostMultiplier = Math.Pow(selectedCompanion.Companion.LevelCostIncrease, selectedCompanion.CompanionLevel - 1);
                levelCost = selectedCompanion.Companion.BaseLevelCost * levelCostMultiplier;
            }

            

            if ((ulong)levelCost > profile.Coins)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You only have **{UtilityFunctions.FormatNumber(profile.Coins)}** {EmojiHandler.GetEmoji("coin")}," +
                    $" but you need **{UtilityFunctions.FormatNumber((ulong)levelCost)}** {EmojiHandler.GetEmoji("coin")} to level up {EmojiHandler.GetEmoji(selectedCompanion.Companion.IconName)} **{selectedCompanion.Companion.Name}** to the next level.").Build())
    .ConfigureAwait(false);
                return;
            }

            

            profile.Coins -= (ulong)levelCost;
            selectedCompanion.CompanionLevel += 1;

            await _profileService.Update(ctx, profile);

            await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully leveled {EmojiHandler.GetEmoji(selectedCompanion.Companion.IconName)} **{selectedCompanion.Companion.Name}** to level **{selectedCompanion.CompanionLevel}** for **{UtilityFunctions.FormatNumber((ulong)levelCost)}** {EmojiHandler.GetEmoji("coin")}.").Build())
    .ConfigureAwait(false);
            return;
        }
    }
}
