﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using IdleHeroes.EmbedTemplates;
using IdleHeroes.Models;
using IdleHeroes.Services;
using IdleHeroes.Support;
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
        public async Task Stage(CommandContext ctx, [Description("Collect the rewards you have earned from farming.")] string collect = null)
        {
            try
            {
                int Min = 1;
                int Max = 100;
                Profile profile = await _profileService.FindByDiscordID(ctx).ConfigureAwait(false);
                Stage stage = await _stageService.GetStageFromProfile(profile).ConfigureAwait(false);

                //Reset the last played time to now
                profile.LastPlayed = DateTime.Now;

                //Collect rewards instead
                if (!string.IsNullOrEmpty(collect))
                {
                    await CollectRewards(ctx, profile);
                    return;
                }

                //Calculate idle resources gained
                TimeSpan idleTime = UtilityFunctions.GetIdleTime(profile);


                if (idleTime.TotalMinutes >= 1)
                {
                    profile.IdleXP += (ulong)idleTime.TotalMinutes * stage.XPPerMinute;
                    profile.IdleCoins += (ulong)idleTime.TotalMinutes * stage.CoinsPerMinute;

                    Random radnomFoodNum = new Random();
                    List<int> foodChances = Enumerable
                        .Repeat(0, Convert.ToInt32(idleTime.TotalMinutes))
                        .Select(i => radnomFoodNum.Next(Min, Max))
                        .ToList();
                    profile.IdleFood += (ulong)foodChances.FindAll(x => (ulong)x <= stage.FoodPerMinute).Count();

                    Random radnomGemsNum = new Random();
                    List<int> gemsChances = Enumerable
                        .Repeat(0, Convert.ToInt32(idleTime.TotalMinutes))
                        .Select(i => radnomGemsNum.Next(Min, Max))
                        .ToList();
                    profile.IdleGems += (ulong)gemsChances.FindAll(x => (ulong)x <= stage.GemsDropChancePerMinute).Count();

                    Random radnomRelicNum = new Random();
                    List<int> relicChances = Enumerable
                        .Repeat(0, Convert.ToInt32(idleTime.TotalMinutes))
                        .Select(i => radnomRelicNum.Next(Min, Max))
                        .ToList();
                    profile.IdleRelics += (ulong)relicChances.FindAll(x => (ulong)x <= stage.RelicsDropChancePerMinute).Count();

                    profile.RewardMinutesAlreadyCalculated += Convert.ToInt32(idleTime.TotalMinutes);
                }

                //Save the profile
                await _profileService.Update(ctx, profile);

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

        public async Task CollectRewards(CommandContext ctx, Profile profile)
        {
            try
            {
                //Add rewards to profile
                profile.XP += profile.IdleXP;
                profile.Coins += profile.IdleCoins;
                profile.Food += profile.IdleFood;
                profile.Gems += profile.IdleGems;
                profile.Relics += profile.IdleRelics;

                //Reset the idle rewards
                profile.IdleXP = 0;
                profile.IdleCoins = 0;
                profile.IdleFood = 0;
                profile.IdleGems = 0;
                profile.IdleRelics = 0;
                profile.RewardMinutesAlreadyCalculated = 0;
                profile.LastRewardsCollected = DateTime.Now;

                //Save the profile
                await _profileService.Update(ctx, profile);

                await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, "You have successfully collected your rewards.").Build()).ConfigureAwait(false);
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