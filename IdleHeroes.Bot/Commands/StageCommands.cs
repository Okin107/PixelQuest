using DSharpPlus.CommandsNext;
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
using IdleHeroesDAL.Enums;

namespace IdleHeroes.Commands
{
    public class StageCommands : BaseCommandModule
    {
        IProfileService _profileService;
        IStageService _stageService;

        public StageCommands(IProfileService profileService, IStageService stageService)
        {
            _profileService = profileService;
            _stageService = stageService;
        }

        [Command("stage")]
        [Description("Check the stage you currently are in.")]
        public async Task Stage(CommandContext ctx, [Description("The action you want to perform <fight>.")] string action = null)
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

                Profile profile = await _profileService.FindByDiscordId(ctx).ConfigureAwait(false);

                if (action == "fight")
                {
                    await FightStage(ctx, profile);
                    return;
                }

                await ctx.Channel.SendMessageAsync(embed: StageInfoEmbedTemplate.Show(ctx, profile).Build()).ConfigureAwait(false);
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

        private async Task FightStage(CommandContext ctx, Profile profile)
        {
            bool battleFinished = false;
            bool battleWon = false;

            List<TeamPositionEnum> defeatedTeamPositions = new List<TeamPositionEnum>();
            List<TeamPositionEnum> defeatedEnemyPositions = new List<TeamPositionEnum>();

            Dictionary<TeamPositionEnum, ulong> teamDpsSpread = new Dictionary<TeamPositionEnum, ulong>();
            Dictionary<TeamPositionEnum, ulong> enemyDpsSpread = new Dictionary<TeamPositionEnum, ulong>();

            for (int i = 1; i <= profile.Stage.TimeToBeat.TotalSeconds; i++)
            {
                if (defeatedTeamPositions.Count == profile.Team.Companions.Count + 1)
                {
                    battleFinished = true;
                    battleWon = false;
                    break;
                }

                #region TeamDPS
                foreach (TeamCompanion companion in profile.Team.Companions.OrderBy(x => x.TeamPosition))
                {
                    //not dead companions attack
                    if (!defeatedTeamPositions.Contains(companion.TeamPosition))
                    {
                        foreach (StageEnemy enemy in profile.Stage.Enemies.OrderBy(x => x.Position))
                        {
                            //Attack non dead enemies
                            if (!defeatedEnemyPositions.Contains(enemy.Position))
                            {
                                if (teamDpsSpread.ContainsKey(enemy.Position))
                                {
                                    teamDpsSpread[enemy.Position] += CompanionHelper.CalculateAttribute(companion.OwnedCompanion, CompanionAttributeEnum.DPS);
                                }
                                else
                                {
                                    teamDpsSpread[enemy.Position] = CompanionHelper.CalculateAttribute(companion.OwnedCompanion, CompanionAttributeEnum.DPS);
                                }

                                break; //Exit the enemy loop once dps is applied
                            }
                        }
                    }
                }

                //Add hero damage
                if (!defeatedTeamPositions.Contains(profile.Team.HeroTeamPosition))
                {
                    foreach (StageEnemy enemy in profile.Stage.Enemies.OrderBy(x => x.Position))
                    {
                        //Attack non dead enemies
                        if (!defeatedEnemyPositions.Contains(enemy.Position))
                        {
                            if (teamDpsSpread.ContainsKey(enemy.Position))
                            {
                                teamDpsSpread[enemy.Position] += profile.BaseDPS;
                            }
                            else
                            {
                                teamDpsSpread[enemy.Position] = profile.BaseDPS;
                            }
                            break; //Exit the enemy loop once dps is applied
                        }
                    }
                }

                //Mark defeated enemies
                foreach (StageEnemy enemy in profile.Stage.Enemies.OrderBy(x => x.Position))
                {
                    ulong teamDpsFound = teamDpsSpread.ContainsKey(enemy.Position) ? teamDpsSpread[enemy.Position] : 0;

                    if (teamDpsFound >= enemy.Enemy.HP && !defeatedEnemyPositions.Contains(enemy.Position))
                    {
                        defeatedEnemyPositions.Add(enemy.Position);
                    }
                }
                #endregion

                if (defeatedEnemyPositions.Count == profile.Stage.Enemies.Count)
                {
                    battleFinished = true;
                    battleWon = true;
                    break;
                }

                #region EnemyDPS
                foreach (StageEnemy enemy in profile.Stage.Enemies.OrderBy(x => x.Position))
                {
                    //not dead enemies
                    if (!defeatedEnemyPositions.Contains(enemy.Position))
                    {
                        if(profile.Team.Companions.Count > 0)
                        {
                            foreach (TeamCompanion companion in profile.Team.Companions.OrderBy(x => x.TeamPosition))
                            {
                                bool dmgApplied = false;

                                //Attack non dead companions
                                if (!defeatedTeamPositions.Contains(companion.TeamPosition) && companion.TeamPosition < profile.Team.HeroTeamPosition)
                                {
                                    dmgApplied = true;
                                    if (enemyDpsSpread.ContainsKey(companion.TeamPosition))
                                    {
                                        enemyDpsSpread[companion.TeamPosition] += enemy.Enemy.DPS;
                                    }
                                    else
                                    {
                                        enemyDpsSpread[companion.TeamPosition] = enemy.Enemy.DPS;
                                    }
                                    break; //Exit the enemy loop once dps is applied
                                }

                                //Check if Hero is attacked first
                                if (!dmgApplied)
                                {
                                    if (enemyDpsSpread.ContainsKey(profile.Team.HeroTeamPosition))
                                    {
                                        enemyDpsSpread[profile.Team.HeroTeamPosition] += enemy.Enemy.DPS;
                                    }
                                    else
                                    {
                                        enemyDpsSpread[profile.Team.HeroTeamPosition] = enemy.Enemy.DPS;
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (enemyDpsSpread.ContainsKey(profile.Team.HeroTeamPosition))
                            {
                                enemyDpsSpread[profile.Team.HeroTeamPosition] += enemy.Enemy.DPS;
                            }
                            else
                            {
                                enemyDpsSpread[profile.Team.HeroTeamPosition] = enemy.Enemy.DPS;
                            }
                            break;
                        }
                        
                    }
                }

                //Mark defeated companions
                foreach (TeamCompanion companion in profile.Team.Companions.OrderBy(x => x.TeamPosition))
                {
                    ulong enemyDpsFound = enemyDpsSpread.ContainsKey(companion.TeamPosition) ? enemyDpsSpread[companion.TeamPosition] : 0;

                    if (enemyDpsFound >= companion.OwnedCompanion.Companion.HP && !defeatedTeamPositions.Contains(companion.TeamPosition))
                    {
                        defeatedTeamPositions.Add(companion.TeamPosition);
                    }
                }

                //Check if hero is defeated
                ulong heroDpsFound = enemyDpsSpread.ContainsKey(profile.Team.HeroTeamPosition) ? enemyDpsSpread[profile.Team.HeroTeamPosition] : 0;

                if (heroDpsFound >= profile.HP)
                {
                    defeatedTeamPositions.Add(profile.Team.HeroTeamPosition);
                }
                #endregion
            }

            if(battleWon)
            {
                //Temp stage soft cap to 10 stages
                if(profile.Stage.Number >= 10)
                {
                    await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have won the battle and beat the maximum stage. (This is a temporary battle message. The detailed message is under development)").Build())
                   .ConfigureAwait(false);
                }

                //Increment stage
                profile.Stage = await _stageService.GetStageFromNumber(profile.Stage.Number + 1);
                await _profileService.Update(ctx, profile);

                await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have won the battle and moved to **Stage {profile.Stage.Number}**. (This is a temporary battle message. The detailed message is under development)").Build())
                   .ConfigureAwait(false);
            }
            else
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You did not win the battle. (This is a temporary battle message. The detailed message is under development)").Build())
                   .ConfigureAwait(false);
            }
            
        }

        [Command("farm")]
        [Description("Check the stage you currently are in.")]
        public async Task Farm(CommandContext ctx, [Description("Collect the rewards you have earned from farming.")] string collect = null)
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

                Profile profile = await _profileService.FindByDiscordId(ctx).ConfigureAwait(false);

                //Reset the last played time to now
                profile.LastPlayed = DateTime.Now;

                CalculateIdleResources(ctx, profile);

                //Collect rewards instead
                if (!string.IsNullOrEmpty(collect) && collect.Equals("collect"))
                {
                    await CollectRewards(ctx, profile);
                    return;
                }

                if (!string.IsNullOrEmpty(collect))
                {
                    await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"Please make sure you have used the correct command syntax. Please check `.help` for more information.").Build())
                        .ConfigureAwait(false);
                    return;
                }

                //Save the profile
                await _profileService.Update(ctx, profile);

                await ctx.Channel.SendMessageAsync(embed: FarmEmbedTemplate.Show(ctx, profile).Build()).ConfigureAwait(false);
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

        private void CalculateIdleResources(CommandContext ctx, Profile profile)
        {
            int Min = 1;
            int Max = 100;

            //Calculate idle resources gained
            TimeSpan idleTime = UtilityFunctions.GetIdleTime(profile);

            if (idleTime.TotalMinutes >= 1)
            {
                profile.IdleXP += (ulong)idleTime.TotalMinutes * profile.Stage.XPPerMinute;
                profile.IdleCoins += (ulong)idleTime.TotalMinutes * profile.Stage.CoinsPerMinute;

                Random radnomFoodNum = new Random();
                List<int> foodChances = Enumerable
                    .Repeat(0, Convert.ToInt32(idleTime.TotalMinutes))
                    .Select(i => radnomFoodNum.Next(Min, Max))
                    .ToList();
                profile.IdleFood += (ulong)foodChances.FindAll(x => (ulong)x <= profile.Stage.FoodChancePerMinute).Count() * profile.Stage.FoodAmount;

                Random radnomGemsNum = new Random();
                List<int> gemsChances = Enumerable
                    .Repeat(0, Convert.ToInt32(idleTime.TotalMinutes))
                    .Select(i => radnomGemsNum.Next(Min, Max))
                    .ToList();
                profile.IdleGems += (ulong)gemsChances.FindAll(x => (ulong)x <= profile.Stage.GemsDropChancePerMinute).Count() * profile.Stage.GemsAmount;

                Random radnomRelicNum = new Random();
                List<int> relicChances = Enumerable
                    .Repeat(0, Convert.ToInt32(idleTime.TotalMinutes))
                    .Select(i => radnomRelicNum.Next(Min, Max))
                    .ToList();
                profile.IdleRelics += (ulong)relicChances.FindAll(x => (ulong)x <= profile.Stage.RelicsDropChancePerMinute).Count() * profile.Stage.RelicsAmount;

                profile.RewardMinutesAlreadyCalculated += Convert.ToInt32(idleTime.TotalMinutes);
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

                //Set the rewards template here
                string rewardsString = $"**You have successfully collected your rewards.**" +
                    $"\n" +
                    $"\n{EmojiHandler.GetEmoji("xp")} {UtilityFunctions.FormatNumber(profile.XP)} **(+{UtilityFunctions.FormatNumber(profile.IdleXP)})**" +
                    $"\n{EmojiHandler.GetEmoji("coin")} {UtilityFunctions.FormatNumber(profile.Coins)} **(+{UtilityFunctions.FormatNumber(profile.IdleCoins)})**" +
                    $"\n{EmojiHandler.GetEmoji("food")} {UtilityFunctions.FormatNumber(profile.Food)} **(+{UtilityFunctions.FormatNumber(profile.IdleFood)})**" +
                    $"\n{EmojiHandler.GetEmoji("gem")} {UtilityFunctions.FormatNumber(profile.Gems)} **(+{UtilityFunctions.FormatNumber(profile.IdleGems)})**" +
                    $"\n{EmojiHandler.GetEmoji("relic")} {UtilityFunctions.FormatNumber(profile.Relics)} **(+{UtilityFunctions.FormatNumber(profile.IdleRelics)})**";

                //Reset the idle rewards
                profile.IdleXP = 0;
                profile.IdleCoins = 0;
                profile.IdleFood = 0;
                profile.IdleGems = 0;
                profile.IdleRelics = 0;

                TimeSpan lastRewardCollectedTime = DateTime.Now - profile.LastRewardsCollected;

                if (lastRewardCollectedTime.TotalSeconds / 60 >= 1)
                {
                    profile.RewardMinutesAlreadyCalculated = 0;
                    profile.LastRewardsCollected = DateTime.Now;
                }

                //Save the profile
                await _profileService.Update(ctx, profile);

                await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, rewardsString).Build()).ConfigureAwait(false);
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
