using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using IdleHeroes.EmbedTemplates;
using IdleHeroes.Models;
using IdleHeroes.Services;
using IdleHeroes.Support;
using IdleHeroesDAL.Enums;
using IdleHeroesDAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static IdleHeroes.Support.ProfileHelper;

namespace IdleHeroes.Commands
{
    public class ProfileCommands : BaseCommandModule
    {
        IProfileService _profileService = null;
        IGearService _gearService = null;
        public ProfileCommands(IProfileService profileService, IGearService gearService)
        {
            _profileService = profileService;
            _gearService = gearService;
        }

        [Command("create")]
        [Description("Create a new profile for the game.")]
        public async Task Create(CommandContext ctx, [Description("The display name for your profile.")] string username = null)
        {
            try
            {
                //Check if username is empty
                if (String.IsNullOrEmpty(username))
                {
                    await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"Your **username** cannot be empty. Use `.help create` to find out more.").Build())
                    .ConfigureAwait(false);
                    return;
                }

                //Check if profile exits
                if (await _profileService.ProfileExists(ctx))
                {
                    await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You already have a Profile.").Build())
                    .ConfigureAwait(false);
                    return;
                }

                //Create profile
                await _profileService.Add(ctx, username);

                //Send message to the channel
                await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"Welcome **{username}**. Use `.profile` to check your stats and start playing.").Build())
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

        [Command("profile")]
        [Description("View your profile or the profile of another player.")]
        public async Task Profile(CommandContext ctx, [Description("The username you want to view the profile for. Leaving this empty will show your own profile.")] string username = null)
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

                Profile profile;

                //TODO: Turn this into multiple selection if there are many results. User interactivity methods
                if (!string.IsNullOrEmpty(username))
                {
                    profile = await _profileService.FindByUsername(ctx, username);
                }
                else
                {
                    profile = await _profileService.FindByDiscordId(ctx);

                    //Check and refresh battle retries=
                    if (DateTime.Now.Date > profile.LastRetriesRefresh.Date)
                    {
                        await _profileService.RefreshBattleRetries(ctx, profile);
                    }
                }

                if (profile == null)
                {
                    await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, "There is no **Profile** with that **username**.").Build())
                        .ConfigureAwait(false);
                }
                else
                {
                    await ctx.Channel.SendMessageAsync(embed: ProfileEmbedTemplate.Get(ctx, profile).Build())
                .ConfigureAwait(false);
                }
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

        [Command("hero")]
        [Description("View and manage your Hero.")]
        public async Task Hero(CommandContext ctx)
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

                //Reset the last played time to now
                profile.LastPlayed = DateTime.Now;

                await ctx.Channel.SendMessageAsync(embed: HeroEmbedTemplate.Get(ctx, profile).Build())
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

        [Command("skills")]
        [Description("View and manage your Hero's skills.")]
        public async Task Skills(CommandContext ctx, [Description("The ID of the Skill that you want to purchase. This is the number in front of the each Skill. You can also reset your skill points by typing <reset>.")] string skillId = null)
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

                //Buy skill
                if (!string.IsNullOrEmpty(skillId))
                {
                    if (Enum.TryParse(skillId, out ProfileSkillTypeEnum skillType))
                    {
                        await UpgradeSkill(ctx, profile, skillType);
                    }
                    else
                    {
                        if(skillId == "reset")
                        {
                            await ResetSkills(ctx, profile);
                            return;
                        }
                        else
                        {
                            await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"The **Skill ID** is wrong. Use `.help skills` to find out more.").Build())
                .ConfigureAwait(false);
                            return;
                        }
                    }
                }

                await ctx.Channel.SendMessageAsync(embed: SkillsEmbedTemplate.Show(ctx, profile).Build())
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

        private async Task ResetSkills(CommandContext ctx, Profile profile)
        {
            if (profile.SkillPointsSpent == 0)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You have not spent any {EmojiHandler.GetEmoji("sp")}, so you have nothing to reset.").Build())
                  .ConfigureAwait(false);
                return;
            }

            if (profile.Gems < 25)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You have **{profile.Gems}** {EmojiHandler.GetEmoji("gem")}," +
                   $" but you need **25** {EmojiHandler.GetEmoji("gem")} to reset your {EmojiHandler.GetEmoji("sp")}.").Build())
               .ConfigureAwait(false);
                return;
            }

            ProfileLevelData profileLevelData = CalculateProfileData(profile);

            profile.SkillPointsSpent = 0;
            profile.DPSBoostLevel = 0;
            profile.HPBoostLevel = 0;
            profile.ArmorBoostLevel = 0;
            profile.AccuracyBoostLevel = 0;
            profile.AgilityBoostLevel = 0;
            profile.Gems -= 25;

            await _profileService.Update(ctx, profile);

            await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully reset all your {EmojiHandler.GetEmoji("sp")} by using **25** {EmojiHandler.GetEmoji("gem")}.").Build()).ConfigureAwait(false);
        }

        private async Task UpgradeSkill(CommandContext ctx, Profile profile, ProfileSkillTypeEnum skillId)
        {
            ProfileLevelData profileLevelData = CalculateProfileData(profile);
            double skillPoints = profileLevelData.Level - profile.SkillPointsSpent - 1;
            double skillCost = 999999;

            switch (skillId)
            {
                case ProfileSkillTypeEnum.DPS:
                    skillCost = Math.Round(1 * Math.Pow(profile.BoostCostIncrease, profile.DPSBoostLevel - 1), 0);

                    if (skillCost <= skillPoints)
                    {
                        profile.SkillPointsSpent += skillCost;
                        profile.DPSBoostLevel++;

                        await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully upgraded **{ProfileSkillTypeEnum.DPS}** to level **{profile.DPSBoostLevel}** by using **{skillCost}** {EmojiHandler.GetEmoji("sp")}.").Build()).ConfigureAwait(false);
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You have **{skillPoints}** {EmojiHandler.GetEmoji("sp")}," +
                    $" but you need **{skillCost}** {EmojiHandler.GetEmoji("sp")} to upgrade **{ProfileSkillTypeEnum.DPS}**.").Build())
                .ConfigureAwait(false);
                    }
                    break;
                case ProfileSkillTypeEnum.HP:
                    skillCost = Math.Round(1 * Math.Pow(profile.BoostCostIncrease, profile.HPBoostLevel - 1), 0);

                    if (skillCost <= skillPoints)
                    {
                        profile.SkillPointsSpent += skillCost;
                        profile.HPBoostLevel++;

                        await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully upgraded **{ProfileSkillTypeEnum.HP}** to level **{profile.HPBoostLevel}** by using **{skillCost}** {EmojiHandler.GetEmoji("sp")}.").Build()).ConfigureAwait(false);
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You have **{skillPoints}** {EmojiHandler.GetEmoji("sp")}," +
                    $" but you need **{skillCost}** {EmojiHandler.GetEmoji("sp")} to upgrade **{ProfileSkillTypeEnum.HP}**.").Build())
                .ConfigureAwait(false);
                    }
                    break;
                case ProfileSkillTypeEnum.Armor:
                    skillCost = Math.Round(1 * Math.Pow(profile.BoostCostIncrease, profile.ArmorBoostLevel - 1), 0);

                    if (skillCost <= skillPoints)
                    {
                        profile.SkillPointsSpent += skillCost;
                        profile.ArmorBoostLevel++;

                        await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully upgraded **{ProfileSkillTypeEnum.Armor}** to level **{profile.ArmorBoostLevel}** by using **{skillCost}** {EmojiHandler.GetEmoji("sp")}.").Build()).ConfigureAwait(false);
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You have **{skillPoints}** {EmojiHandler.GetEmoji("sp")}," +
                    $" but you need **{skillCost}** {EmojiHandler.GetEmoji("sp")} to upgrade **{ProfileSkillTypeEnum.Armor}**.").Build())
                .ConfigureAwait(false);
                    }
                    break;
                case ProfileSkillTypeEnum.Accuracy:
                    skillCost = Math.Round(1 * Math.Pow(profile.BoostCostIncrease, profile.AccuracyBoostLevel - 1), 0);

                    if (skillCost <= skillPoints)
                    {
                        profile.SkillPointsSpent += skillCost;
                        profile.AccuracyBoostLevel++;

                        await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully upgraded **{ProfileSkillTypeEnum.Accuracy}** to level **{profile.AccuracyBoostLevel}** by using **{skillCost}** {EmojiHandler.GetEmoji("sp")}.").Build()).ConfigureAwait(false);
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You have **{skillPoints}** {EmojiHandler.GetEmoji("sp")}," +
                    $" but you need **{skillCost}** {EmojiHandler.GetEmoji("sp")} to upgrade **{ProfileSkillTypeEnum.Accuracy}**.").Build())
                .ConfigureAwait(false);
                    }
                    break;
                case ProfileSkillTypeEnum.Agility:
                    skillCost = Math.Round(1 * Math.Pow(profile.BoostCostIncrease, profile.AgilityBoostLevel - 1), 0);

                    if (skillCost <= skillPoints)
                    {
                        profile.SkillPointsSpent += skillCost;
                        profile.AgilityBoostLevel++;

                        await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully upgraded **{ProfileSkillTypeEnum.Agility}** to level **{profile.AgilityBoostLevel}** by using **{skillCost}** {EmojiHandler.GetEmoji("sp")}.").Build()).ConfigureAwait(false);
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You have **{skillPoints}** {EmojiHandler.GetEmoji("sp")}," +
                    $" but you need **{skillCost}** {EmojiHandler.GetEmoji("sp")} to upgrade **{ProfileSkillTypeEnum.Agility}**.").Build())
                .ConfigureAwait(false);
                    }
                    break;
            }

            await _profileService.Update(ctx, profile);
        }

        [Command("gear")]
        [Description("View and manage your Hero's gear.")]
        public async Task Gears(CommandContext ctx, [Description("The ID of the Gear that you want to purchase/upgrade. This is the number in front of the each Gear.")] string gearId = null)
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

                //Check if gears are unlocked
                if(profile.Stage.Number < 30)
                {
                    await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"Hero's Gear unlock at stage 30.").Build())
                        .ConfigureAwait(false);
                    return;
                }

                List<Gear> gears = await _gearService.GetAll(ctx);
                //Reset the last played time to now
                profile.LastPlayed = DateTime.Now;

                //Buy gear
                if (!string.IsNullOrEmpty(gearId))
                {
                    if (Int32.TryParse(gearId, out int gearIdint))
                    {
                        await UpgradeGear(ctx, profile, gearIdint);
                    }
                    else
                    {
                        if (gearId == "reset")
                        {
                           //Placeholder for other options
                            return;
                        }
                        else
                        {
                            await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"The **Gear ID** is wrong. Use `.help gear` to find out more.").Build())
                .ConfigureAwait(false);
                            return;
                        }
                    }
                }

                await ctx.Channel.SendMessageAsync(embed: GearEmbedTemplate.Show(ctx, profile, gears).Build())
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

        private async Task UpgradeGear(CommandContext ctx, Profile profile, int gearId)
        {
            OwnedGear selectedGear = profile.OwnedGears.Find(x => x.Gear.Id == gearId);

            if (selectedGear == null)
            {
                Gear gear = await _gearService.GetById(ctx, gearId);

                //Unlock the gear
                if (gear.BaseLevelCost > profile.Relics)
                {
                    await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You only have **{UtilityFunctions.FormatNumber(profile.Relics)}** {EmojiHandler.GetEmoji("relic")}," +
                        $" but you need **{UtilityFunctions.FormatNumber(gear.BaseLevelCost)}** {EmojiHandler.GetEmoji("relic")} to unlock {EmojiHandler.GetEmoji(gear.IconName)} **{gear.Type}**.").Build())
        .ConfigureAwait(false);
                    return;
                }

                profile.Relics -= gear.BaseLevelCost;
                profile.OwnedGears.Add(new OwnedGear()
                {
                    Level = 1,
                    Gear = gear
                });

                await _profileService.Update(ctx, profile);

                await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully unlocked {EmojiHandler.GetEmoji(gear.IconName)} **{gear.Type}** for **{UtilityFunctions.FormatNumber(gear.BaseLevelCost)}** {EmojiHandler.GetEmoji("relic")}.").Build())
        .ConfigureAwait(false);
                return;
            }

            //Max level reached
            if (selectedGear.Level == selectedGear.Gear.MaxLevel)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"{EmojiHandler.GetEmoji(selectedGear.Gear.IconName)} **{selectedGear.Gear.Type}** has reached the maximum level.").Build())
    .ConfigureAwait(false);
                return;
            }

            //Check if it can be purchased
            double levelCost = selectedGear.Gear.BaseLevelCost;

            if (selectedGear.Level > 1)
            {
                levelCost = GearHelper.NextLevelCost(selectedGear.Gear, selectedGear.Level + 1);
            }

            if (levelCost > profile.Relics)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You only have **{UtilityFunctions.FormatNumber(profile.Relics)}** {EmojiHandler.GetEmoji("relic")}," +
                    $" but you need **{UtilityFunctions.FormatNumber(levelCost)}** {EmojiHandler.GetEmoji("relic")} to upgrade {EmojiHandler.GetEmoji(selectedGear.Gear.IconName)} **{selectedGear.Gear.Type}**.").Build())
    .ConfigureAwait(false);
                return;
            }


            profile.Relics -= levelCost;
            selectedGear.Level += 1;

            await _profileService.Update(ctx, profile);

            await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully upgraded {EmojiHandler.GetEmoji(selectedGear.Gear.IconName)} **{selectedGear.Gear.Type}** to level **{selectedGear.Level}** for **{UtilityFunctions.FormatNumber((ulong)levelCost)}** {EmojiHandler.GetEmoji("relic")}.").Build())
    .ConfigureAwait(false);
            return;
        }
    }
}
