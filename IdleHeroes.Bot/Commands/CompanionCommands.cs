using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using IdleHeroes.EmbedTemplates;
using IdleHeroes.Models;
using IdleHeroes.Services;
using IdleHeroes.Support;
using IdleHeroesDAL.Enums;
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
        public async Task CompanionCodex(CommandContext ctx, [Description("Apply a filter to the codex list. <rarity> <stats>")] string filter = null)
        {
            try
            {
                InteractivityExtension interactivity = ctx.Client.GetInteractivity();
                List<Companion> companionList = await _companionService.GetCompanions(RarityTierEnum.Common);

                if (!string.IsNullOrEmpty(filter))
                {
                    if(filter == "stats")
                    {
                        await ctx.Channel.SendMessageAsync(embed: CodexStatsEmbedTemplate.Show(ctx).Build())
                   .ConfigureAwait(false);
                        return;
                    }
                    if(filter == "rare")
                    {
                        companionList = await _companionService.GetCompanions(RarityTierEnum.Rare);
                    }
                    if (filter == "epic")
                    {
                        companionList = await _companionService.GetCompanions(RarityTierEnum.Epic);
                    }
                    if (filter == "legendary")
                    {
                        companionList = await _companionService.GetCompanions(RarityTierEnum.Legendary);
                    }
                    if (filter == "mythic")
                    {
                        companionList = await _companionService.GetCompanions(RarityTierEnum.Mythic);
                    }
                }


                await interactivity.SendPaginatedMessageAsync(ctx.Channel, ctx.User, CodexEmbedTemplate.Show(ctx, companionList), timeoutoverride: TimeSpan.FromMinutes(5)).ConfigureAwait(false);
                //await ctx.Channel.SendMessageAsync(embed: CodexEmbedTemplate.Show(ctx, companionList).Build())
                //   .ConfigureAwait(false);
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
        public async Task OwnedCompanions(CommandContext ctx, [Description("The Companion ID which you wish to select.")] string companionId = null, [Description("The action you want to take on the selected Companion (<level>, <ascend>)")] string action = null)
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
                InteractivityExtension interactivity = ctx.Client.GetInteractivity();

                Profile profile = await _profileService.FindByDiscordId(ctx);
                List<Companion> companionList = await _companionService.GetCompanions();

                //Reset the last played time to now
                profile.LastPlayed = DateTime.Now;

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
                        await LevelCompanion(ctx, profile, compId);
                        return;
                    }
                    else if (action == "ascend")
                    {
                        await AscendCompanion(ctx, profile, compId);
                        return;
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"Please make sure you have used the correct command syntax. Use `.help comp` to find out more.").Build())
                .ConfigureAwait(false);
                        return;
                    }
                }


                await interactivity.SendPaginatedMessageAsync(ctx.Channel, ctx.User, OwnedCompanionsEmbedTemplate.Show(ctx, profile), timeoutoverride: TimeSpan.FromMinutes(5)).ConfigureAwait(false);
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

        private async Task AscendCompanion(CommandContext ctx, Profile profile, int compId)
        {
            OwnedCompanion selectedCompanion = profile.OwnedCompanions.Find(x => x.Companion.Id == compId);

            if (selectedCompanion == null)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You do not own any Companion with ID **{compId}**. Please make sure you selected the correct **Companion ID**.").Build())
    .ConfigureAwait(false);
                return;
            }

            //Max ascend reached
            if (selectedCompanion.RarirtyTier == IdleHeroesDAL.Enums.RarityTierEnum.Mythic)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"{EmojiHandler.GetEmoji(selectedCompanion.Companion.IconName)} **{selectedCompanion.Companion.Name}** has reached the maximum **Ascend Tier**.").Build())
    .ConfigureAwait(false);
                return;
            }

            //Check if companion is at max level
            double maxLevel = (selectedCompanion.Companion.MaxLevel / 5) * (double)selectedCompanion.RarirtyTier;

            if (selectedCompanion.Level < maxLevel)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"{EmojiHandler.GetEmoji(selectedCompanion.Companion.IconName)} **{selectedCompanion.Companion.Name}** has to be at the maximum level of his current **Ascend Tier** in order to be ascended further.").Build())
    .ConfigureAwait(false);
                return;
            }

            //Check if it can be ascended
            double ascendCopiesNeeded = selectedCompanion.Companion.BaseAscendCopiesNeeded * Math.Pow(selectedCompanion.Companion.AscendCopiesTierIncrease, (double)selectedCompanion.RarirtyTier - 1);

            if (selectedCompanion.Copies < ascendCopiesNeeded)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You only have **{selectedCompanion.Copies}** copies," +
                    $" but you need **{ascendCopiesNeeded}** copies to ascend {EmojiHandler.GetEmoji(selectedCompanion.Companion.IconName)} **{selectedCompanion.Companion.Name}** to the next **Ascend Tier**.").Build())
    .ConfigureAwait(false);
                return;
            }

            //selectedCompanion.Copies -= Convert.ToInt32(ascendCopiesNeeded);
            selectedCompanion.RarirtyTier += 1;

            await _profileService.Update(ctx, profile);

            await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully ascended {EmojiHandler.GetEmoji(selectedCompanion.Companion.IconName)} **{selectedCompanion.Companion.Name}** to **{UtilityFunctions.GetTierStars((int)selectedCompanion.RarirtyTier)}** by using **{ascendCopiesNeeded}** of his copies.").Build())
    .ConfigureAwait(false);
            return;
        }

        private async Task LevelCompanion(CommandContext ctx, Profile profile, int compId)
        {
            OwnedCompanion selectedCompanion = profile.OwnedCompanions.Find(x => x.Companion.Id == compId);

            if (selectedCompanion == null)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You do not own any Companion with ID **{compId}**. Please make sure you selected the correct **Companion ID**.").Build())
    .ConfigureAwait(false);
                return;
            }

            //Max level reached
            double maxLevel = (selectedCompanion.Companion.MaxLevel / 5) * (double)selectedCompanion.RarirtyTier;

            if(selectedCompanion.Level == maxLevel)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"{EmojiHandler.GetEmoji(selectedCompanion.Companion.IconName)} **{selectedCompanion.Companion.Name}** has reached the maximum level for this **Ascend Tier**.").Build())
    .ConfigureAwait(false);
                return;
            }

            //Check if it can be purchased
            double levelCost = selectedCompanion.Companion.BaseLevelCost;

            if (selectedCompanion.Level > 1)
            {
                double levelCostMultiplier = Math.Pow(selectedCompanion.Companion.LevelCostIncrease, selectedCompanion.Level - 1);
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
            selectedCompanion.Level += 1;

            await _profileService.Update(ctx, profile);

            await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully leveled {EmojiHandler.GetEmoji(selectedCompanion.Companion.IconName)} **{selectedCompanion.Companion.Name}** to level **{selectedCompanion.Level}** for **{UtilityFunctions.FormatNumber((ulong)levelCost)}** {EmojiHandler.GetEmoji("coin")}.").Build())
    .ConfigureAwait(false);
            return;
        }
    }
}
