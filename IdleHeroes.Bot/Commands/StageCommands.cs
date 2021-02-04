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
        public async Task Stage(CommandContext ctx, [Description("The action you want to perform <fight>.")] string action = null, [Description("A specific stage number that you want to see or fight.")] string stageNumber = null)
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

                //Check and refresh battle retries
                if (DateTime.Now.Day > profile.LastRetriesRefresh.Day)
                {
                    await _profileService.RefreshBattleRetries(ctx, profile);
                }

                Stage stage = null;
                List<Stage> stageList = await _stageService.GetAll();

                //Specific stage
                if (Int32.TryParse(action, out int stageNr))
                {
                    stage = await _stageService.GetStageFromNumber(stageNr).ConfigureAwait(false);

                    if (stageNumber == "fight")
                    {
                        await FightStage(ctx, profile, stage);
                        return;
                    }
                }
                else if (action == "fight")
                {
                    await FightStage(ctx, profile);
                    return;
                }

                await ctx.Channel.SendMessageAsync(embed: StageInfoEmbedTemplate.Show(ctx, profile, stage).Build()).ConfigureAwait(false);
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

        private async Task FightStage(CommandContext ctx, Profile profile, Stage stage = null)
        {
            Stage selectedStage = profile.Stage;

            if (stage != null)
            {
                selectedStage = stage;
            }

            bool battleWon = false;
            bool hasWonCompanion = false;
            int battleSeconds = 1;

            List<TeamPositionEnum> defeatedTeamPositions = new List<TeamPositionEnum>();
            List<TeamPositionEnum> defeatedEnemyPositions = new List<TeamPositionEnum>();

            Dictionary<TeamPositionEnum, double> teamDpsSpread = new Dictionary<TeamPositionEnum, double>();
            Dictionary<TeamPositionEnum, double> enemyDpsSpread = new Dictionary<TeamPositionEnum, double>();

            for (battleSeconds = 1; battleSeconds <= selectedStage.TimeToBeat.TotalSeconds; battleSeconds++)
            {
                if (defeatedTeamPositions.Count == profile.Team.Companions.Count + 1)
                {
                    battleWon = false;
                    break;
                }

                #region TeamDPS
                foreach (TeamCompanion companion in profile.Team.Companions.OrderBy(x => x.TeamPosition))
                {
                    //not dead companions attack
                    if (!defeatedTeamPositions.Contains(companion.TeamPosition))
                    {
                        foreach (StageEnemy enemy in selectedStage.Enemies.OrderBy(x => x.Position))
                        {
                            //Attack non dead enemies
                            if (!defeatedEnemyPositions.Contains(enemy.Position))
                            {
                                //Check if assasin so attack the back
                                if (companion.OwnedCompanion.Companion.Class == CompanionClassesEnum.Assasin)
                                {
                                    StageEnemy lastEnemy = selectedStage.Enemies.OrderBy(x => x.Position).LastOrDefault();

                                    if (teamDpsSpread.ContainsKey(lastEnemy.Position))
                                    {
                                        teamDpsSpread[lastEnemy.Position] += CalculateTeamDPSToApply(companion, enemy);
                                    }
                                    else
                                    {
                                        teamDpsSpread[lastEnemy.Position] = CalculateTeamDPSToApply(companion, enemy);
                                    }

                                    //Check if enemy died and mark it
                                    if (teamDpsSpread[lastEnemy.Position] >= CompanionHelper.CalculateAttribute(lastEnemy, CompanionAttributeEnum.HP) && !defeatedEnemyPositions.Contains(lastEnemy.Position))
                                    {
                                        defeatedEnemyPositions.Add(lastEnemy.Position);
                                    }
                                }
                                else
                                {
                                    if (teamDpsSpread.ContainsKey(enemy.Position))
                                    {
                                        teamDpsSpread[enemy.Position] += CalculateTeamDPSToApply(companion, enemy);
                                    }
                                    else
                                    {
                                        teamDpsSpread[enemy.Position] = CalculateTeamDPSToApply(companion, enemy);
                                    }

                                    //Check if enemy died and mark it
                                    if (teamDpsSpread[enemy.Position] >= CompanionHelper.CalculateAttribute(enemy, CompanionAttributeEnum.HP) && !defeatedEnemyPositions.Contains(enemy.Position))
                                    {
                                        defeatedEnemyPositions.Add(enemy.Position);
                                    }
                                }

                                break; //Exit the enemy loop once dps is applied
                            }
                        }
                    }
                }

                //Add hero damage
                if (!defeatedTeamPositions.Contains(profile.Team.HeroTeamPosition))
                {
                    foreach (StageEnemy enemy in selectedStage.Enemies.OrderBy(x => x.Position))
                    {
                        //Attack non dead enemies
                        if (!defeatedEnemyPositions.Contains(enemy.Position))
                        {
                            if (teamDpsSpread.ContainsKey(enemy.Position))
                            {
                                teamDpsSpread[enemy.Position] += CalculateHeroDPSToApply(profile, enemy);
                                // ProfileHelper.CalculateAttribute(profile, CompanionAttributeEnum.DPS);
                            }
                            else
                            {
                                teamDpsSpread[enemy.Position] = CalculateHeroDPSToApply(profile, enemy);
                            }

                            //Check if enemy died and mark it
                            if (teamDpsSpread[enemy.Position] >= CompanionHelper.CalculateAttribute(enemy, CompanionAttributeEnum.HP) && !defeatedEnemyPositions.Contains(enemy.Position))
                            {
                                defeatedEnemyPositions.Add(enemy.Position);
                            }
                            break; //Exit the enemy loop once dps is applied
                        }
                    }
                }
                #endregion

                if (defeatedEnemyPositions.Count == selectedStage.Enemies.Count)
                {
                    battleWon = true;
                    break;
                }

                #region EnemyDPS
                foreach (StageEnemy enemy in selectedStage.Enemies.OrderBy(x => x.Position))
                {
                    //not dead enemies
                    if (!defeatedEnemyPositions.Contains(enemy.Position))
                    {
                        if (profile.Team.Companions.Count > 0)
                        {
                            foreach (TeamCompanion companion in profile.Team.Companions.OrderBy(x => x.TeamPosition))
                            {
                                bool companionDmgApplied = false;
                                bool attackHero = true;

                                //Check if another companion is before hero
                                List<TeamCompanion> companionsFound = profile.Team.Companions.Where(x => x.TeamPosition < profile.Team.HeroTeamPosition).ToList();

                                if (companion.TeamPosition > profile.Team.HeroTeamPosition && defeatedTeamPositions.Contains(profile.Team.HeroTeamPosition))
                                {
                                    attackHero = false;
                                }
                                else if (companionsFound.Any())
                                {
                                    foreach (TeamCompanion companionFound in companionsFound)
                                    {
                                        if (!defeatedTeamPositions.Contains(companionFound.TeamPosition))
                                        {
                                            attackHero = false;
                                            break;
                                        }

                                    }
                                }

                                //Attack companions
                                if (!defeatedTeamPositions.Contains(companion.TeamPosition) && !attackHero)
                                {
                                    companionDmgApplied = true;

                                    if (enemy.Companion.Class == CompanionClassesEnum.Assasin)
                                    {
                                        TeamCompanion lastCompanion = profile.Team.Companions.OrderBy(x => x.TeamPosition).LastOrDefault();

                                        if (enemyDpsSpread.ContainsKey(lastCompanion.TeamPosition))
                                        {
                                            enemyDpsSpread[lastCompanion.TeamPosition] += CalculateEnemyDPSToApply(enemy, companion);
                                        }
                                        else
                                        {
                                            enemyDpsSpread[lastCompanion.TeamPosition] = CalculateEnemyDPSToApply(enemy, companion);
                                        }

                                        if (enemyDpsSpread[lastCompanion.TeamPosition] >= CompanionHelper.CalculateAttribute(lastCompanion.OwnedCompanion, CompanionAttributeEnum.HP) && !defeatedTeamPositions.Contains(lastCompanion.TeamPosition))
                                        {
                                            defeatedTeamPositions.Add(lastCompanion.TeamPosition);
                                        }
                                    }
                                    else
                                    {
                                        if (enemyDpsSpread.ContainsKey(companion.TeamPosition))
                                        {
                                            enemyDpsSpread[companion.TeamPosition] += CalculateEnemyDPSToApply(enemy, companion);
                                        }
                                        else
                                        {
                                            enemyDpsSpread[companion.TeamPosition] = CalculateEnemyDPSToApply(enemy, companion);
                                        }

                                        if (enemyDpsSpread[companion.TeamPosition] >= CompanionHelper.CalculateAttribute(companion.OwnedCompanion, CompanionAttributeEnum.HP) && !defeatedTeamPositions.Contains(companion.TeamPosition))
                                        {
                                            defeatedTeamPositions.Add(companion.TeamPosition);
                                        }
                                    }

                                    break; //Exit the enemy loop once dps is applied
                                }
                                else
                                {
                                    attackHero = true;
                                }

                                //Attack hero
                                if (!companionDmgApplied && attackHero && !defeatedTeamPositions.Contains(profile.Team.HeroTeamPosition))
                                {
                                    if (enemyDpsSpread.ContainsKey(profile.Team.HeroTeamPosition))
                                    {
                                        enemyDpsSpread[profile.Team.HeroTeamPosition] += CalculateEnemyDPSToApply(enemy, profile);
                                    }
                                    else
                                    {
                                        enemyDpsSpread[profile.Team.HeroTeamPosition] = CalculateEnemyDPSToApply(enemy, profile);
                                    }

                                    if (enemyDpsSpread[profile.Team.HeroTeamPosition] >= ProfileHelper.CalculateAttribute(profile, CompanionAttributeEnum.HP) && !defeatedTeamPositions.Contains(profile.Team.HeroTeamPosition))
                                    {
                                        defeatedTeamPositions.Add(profile.Team.HeroTeamPosition);
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (enemyDpsSpread.ContainsKey(profile.Team.HeroTeamPosition))
                            {
                                enemyDpsSpread[profile.Team.HeroTeamPosition] += CalculateEnemyDPSToApply(enemy, profile);
                            }
                            else
                            {
                                enemyDpsSpread[profile.Team.HeroTeamPosition] = CalculateEnemyDPSToApply(enemy, profile);
                            }

                            if (enemyDpsSpread[profile.Team.HeroTeamPosition] >= ProfileHelper.CalculateAttribute(profile, CompanionAttributeEnum.HP) && !defeatedTeamPositions.Contains(profile.Team.HeroTeamPosition))
                            {
                                defeatedTeamPositions.Add(profile.Team.HeroTeamPosition);
                            }
                        }

                    }
                }
                #endregion
            }

            if (battleWon)
            {
                List<Stage> stages = await _stageService.GetAll();

                //If you have no more retries, quit
                if (selectedStage.Number < profile.Stage.Number || profile.Stage.Number == stages.Count)
                {
                    if (profile.BattleRetries == 0)
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You do not have any more **Battle Retries** for today.").Build())
                        .ConfigureAwait(false);
                        return;
                    }
                    else
                    {
                        profile.BattleRetries--;
                    }
                }

                //Give companion reward if it exists
                if (selectedStage.Companion != null)
                {
                    OwnedCompanion ownedCompanionSearch = profile.OwnedCompanions.Find(x => x.Companion.Id == selectedStage.Companion.Id);

                    //If it's a retry, calculate the chance
                    if (selectedStage.Number < profile.Stage.Number || profile.Stage.Number == stages.Count)
                    {
                        double percentChanceToGet = selectedStage.ChanceToGetCompanion;
                        Random random = new Random();
                        int randomChance = random.Next(1, 100);

                        //Got the companion
                        if (randomChance <= percentChanceToGet)
                        {
                            hasWonCompanion = true;

                            OwnedCompanion earnedCompanion = null;
                            if (ownedCompanionSearch == null)
                            {
                                earnedCompanion = new OwnedCompanion()
                                {
                                    Companion = selectedStage.Companion,
                                    Copies = 1,
                                    Level = 1,
                                    RarirtyTier = RarityTierEnum.Common
                                };

                                profile.OwnedCompanions.Add(earnedCompanion);
                            }
                            else
                            {
                                ownedCompanionSearch.Copies += 1;
                            }
                        }
                    }
                    else
                    {
                        hasWonCompanion = true;
                        OwnedCompanion earnedCompanion = null;
                        if (ownedCompanionSearch == null)
                        {
                            earnedCompanion = new OwnedCompanion()
                            {
                                Companion = selectedStage.Companion,
                                Copies = 1,
                                Level = 1,
                                RarirtyTier = RarityTierEnum.Common
                            };

                            profile.OwnedCompanions.Add(earnedCompanion);
                        }
                        else
                        {
                            ownedCompanionSearch.Copies += 1;
                        }
                    }
                }

                //Give rest of rewards
                profile.XP += selectedStage.StaticXP;
                profile.Coins += selectedStage.StaticCoins;
                profile.Food += selectedStage.StaticFood;
                profile.Gems += selectedStage.StaticGems;
                profile.Relics += selectedStage.StaticRelics;

                //Increment stage
                if (profile.Stage.Number < stages.Count && selectedStage.Number >= profile.Stage.Number)
                {
                    profile.Stage = await _stageService.GetStageFromNumber(selectedStage.Number + 1);
                }

                await _profileService.Update(ctx, profile);
            }

            await ctx.Channel.SendMessageAsync(embed: StageFightResultEmbedTemplate.Show(ctx, profile, battleWon, defeatedTeamPositions, defeatedEnemyPositions, teamDpsSpread, enemyDpsSpread, battleSeconds - 1, selectedStage, hasWonCompanion).Build())
                   .ConfigureAwait(false);
        }

        private double CalculateEnemyDPSToApply(StageEnemy enemy, Profile profile)
        {
            double dps = CompanionHelper.CalculateAttribute(enemy, CompanionAttributeEnum.DPS);
            bool attackDodged = false;
            double dodgeChance = 0;
            Random random = new Random();

            //Check if enemy dodged attack
            if (profile.Agility >= 10000)
            {
                dodgeChance = 90;
            }
            else
            {
                dodgeChance = (profile.Agility / 10000) * 100;
            }

            int randomDodge = random.Next(1, 100);

            if (randomDodge <= dodgeChance)
            {
                //Attack dodged
                return 0;
            }

            //Calcualte DPS after armor. Skip if class is ranger (True dmg)
            if (enemy.Companion.Class != CompanionClassesEnum.Ranger)
            {
                double dpsAfterArmorMultiplier = 1;

                if (profile.Armor >= 1000)
                {
                    dpsAfterArmorMultiplier = 0.9;
                }
                else
                {
                    dpsAfterArmorMultiplier = 1 - (profile.Armor / 1000);
                }

                dps = dps * dpsAfterArmorMultiplier;
            }

            //Check if crit hapened
            double critChance = 0;

            if (enemy.Companion.Accuracy >= 10000)
            {
                critChance = 90;
            }
            else
            {
                critChance = (enemy.Companion.Accuracy / 10000) * 100;
            }

            int randomCrit = random.Next(1, 100);

            if (randomCrit <= critChance)
            {
                //Crit happaned
                dps = dps * 3;
            }

            return dps;
        }

        private double CalculateEnemyDPSToApply(StageEnemy enemy, TeamCompanion companion)
        {
            double dps = CompanionHelper.CalculateAttribute(enemy, CompanionAttributeEnum.DPS);
            bool attackDodged = false;
            double dodgeChance = 0;
            Random random = new Random();

            //Check if enemy dodged attack
            if (companion.OwnedCompanion.Companion.Agility >= 10000)
            {
                dodgeChance = 90;
            }
            else
            {
                dodgeChance = (companion.OwnedCompanion.Companion.Agility / 10000) * 100;
            }

            int randomDodge = random.Next(1, 100);

            if (randomDodge <= dodgeChance)
            {
                //Attack dodged
                return 0;
            }

            //Calculate element DPS
            switch (enemy.Companion.Element)
            {
                case ElementTypeEnum.Fire:
                    if (companion.OwnedCompanion.Companion.Element == ElementTypeEnum.Water)
                    {
                        dps = dps / 2;
                    }
                    else if (companion.OwnedCompanion.Companion.Element == ElementTypeEnum.Nature)
                    {
                        dps = dps * 2;
                    }
                    break;
                case ElementTypeEnum.Water:
                    if (companion.OwnedCompanion.Companion.Element == ElementTypeEnum.Nature)
                    {
                        dps = dps / 2;
                    }
                    else if (companion.OwnedCompanion.Companion.Element == ElementTypeEnum.Fire)
                    {
                        dps = dps * 2;
                    }
                    break;
                case ElementTypeEnum.Nature:
                    if (companion.OwnedCompanion.Companion.Element == ElementTypeEnum.Fire)
                    {
                        dps = dps / 2;
                    }
                    else if (companion.OwnedCompanion.Companion.Element == ElementTypeEnum.Water)
                    {
                        dps = dps * 2;
                    }
                    break;
            }

            //Calcualte DPS after armor. Skip if class is ranger (True dmg)
            if (enemy.Companion.Class != CompanionClassesEnum.Ranger)
            {
                double dpsAfterArmorMultiplier = 1;

                if (companion.OwnedCompanion.Companion.Armor >= 1000)
                {
                    dpsAfterArmorMultiplier = 0.9;
                }
                else
                {
                    dpsAfterArmorMultiplier = 1 - (companion.OwnedCompanion.Companion.Armor / 1000);
                }

                dps = dps * dpsAfterArmorMultiplier;
            }

            //Check if crit hapened
            double critChance = 0;

            if (enemy.Companion.Accuracy >= 10000)
            {
                critChance = 90;
            }
            else
            {
                critChance = (enemy.Companion.Accuracy / 10000) * 100;
            }

            int randomCrit = random.Next(1, 100);

            if (randomCrit <= critChance)
            {
                //Crit happaned
                dps = dps * 3;
            }

            return dps;
        }

        private double CalculateHeroDPSToApply(Profile profile, StageEnemy enemy)
        {
            double dps = ProfileHelper.CalculateAttribute(profile, CompanionAttributeEnum.DPS);

            bool attackDodged = false;
            double dodgeChance = 0;
            Random random = new Random();

            //Check if enemy dodged attack
            if (enemy.Companion.Agility >= 10000)
            {
                dodgeChance = 90;
            }
            else
            {
                dodgeChance = (enemy.Companion.Agility / 10000) * 100;
            }

            int randomDodge = random.Next(1, 100);

            if (randomDodge <= dodgeChance)
            {
                //Attack dodged
                return 0;
            }

            //Calcualte DPS after armor. Skip if class is ranger (True dmg)
            double dpsAfterArmorMultiplier = 1;

            if (enemy.Companion.Armor >= 1000)
            {
                dpsAfterArmorMultiplier = 0.9;
            }
            else
            {
                dpsAfterArmorMultiplier = 1 - (enemy.Companion.Armor / 1000);
            }

            dps = dps * dpsAfterArmorMultiplier;

            //Check if crit hapened
            double critChance = 0;

            if (profile.Accuracy >= 10000)
            {
                critChance = 90;
            }
            else
            {
                critChance = (profile.Accuracy / 10000) * 100;
            }

            int randomCrit = random.Next(1, 100);

            if (randomCrit <= critChance)
            {
                //Crit happaned
                dps = dps * 3;
            }

            return dps;
        }

        private double CalculateTeamDPSToApply(TeamCompanion companion, StageEnemy enemy)
        {
            double dps = CompanionHelper.CalculateAttribute(companion.OwnedCompanion, CompanionAttributeEnum.DPS);
            double dodgeChance;
            Random random = new Random();

            //Check if enemy dodged attack
            if (enemy.Companion.Agility >= 10000)
            {
                dodgeChance = 90;
            }
            else
            {
                dodgeChance = (enemy.Companion.Agility / 10000) * 100;
            }

            int randomDodge = random.Next(1, 100);

            if (randomDodge <= dodgeChance)
            {
                //Attack dodged
                return 0;
            }

            //Calculate element DPS
            switch (companion.OwnedCompanion.Companion.Element)
            {
                case ElementTypeEnum.Fire:
                    if (enemy.Companion.Element == ElementTypeEnum.Water)
                    {
                        dps = dps / 2;
                    }
                    else if (enemy.Companion.Element == ElementTypeEnum.Nature)
                    {
                        dps = dps * 2;
                    }
                    break;
                case ElementTypeEnum.Water:
                    if (enemy.Companion.Element == ElementTypeEnum.Nature)
                    {
                        dps = dps / 2;
                    }
                    else if (enemy.Companion.Element == ElementTypeEnum.Fire)
                    {
                        dps = dps * 2;
                    }
                    break;
                case ElementTypeEnum.Nature:
                    if (enemy.Companion.Element == ElementTypeEnum.Fire)
                    {
                        dps = dps / 2;
                    }
                    else if (enemy.Companion.Element == ElementTypeEnum.Water)
                    {
                        dps = dps * 2;
                    }
                    break;
            }

            //Calcualte DPS after armor. Skip if class is ranger (True dmg)
            if (companion.OwnedCompanion.Companion.Class != CompanionClassesEnum.Ranger)
            {
                double dpsAfterArmorMultiplier = 1;

                if (enemy.Companion.Armor >= 1000)
                {
                    dpsAfterArmorMultiplier = 0.9;
                }
                else
                {
                    dpsAfterArmorMultiplier = 1 - (enemy.Companion.Armor / 1000);
                }

                dps = dps * dpsAfterArmorMultiplier;
            }

            //Check if crit hapened
            double critChance = 0;

            if (companion.OwnedCompanion.Companion.Accuracy >= 10000)
            {
                critChance = 90;
            }
            else
            {
                critChance = (companion.OwnedCompanion.Companion.Accuracy / 10000) * 100;
            }

            int randomCrit = random.Next(1, 100);

            if (randomCrit <= critChance)
            {
                //Crit happaned
                dps = dps * 3;
            }

            return dps;
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
                    $"\nStage {profile.Stage.Number} • Idle Time: {UtilityFunctions.GetIdleDisplayTime(profile).ToString("h'h, 'm'm, 's's'")}" +
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
