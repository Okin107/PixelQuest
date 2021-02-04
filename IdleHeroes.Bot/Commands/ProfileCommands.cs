using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using IdleHeroes.EmbedTemplates;
using IdleHeroes.Models;
using IdleHeroes.Services;
using IdleHeroes.Support;
using IdleHeroesDAL.Enums;
using IdleHeroesDAL.Models;
using System;
using System.Threading.Tasks;
using static IdleHeroes.Support.ProfileHelper;

namespace IdleHeroes.Commands
{
    public class ProfileCommands : BaseCommandModule
    {
        IProfileService _profileService = null;

        public ProfileCommands(IProfileService profileService)
        {
            _profileService = profileService;
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
                    if (DateTime.Now.Day > profile.LastRetriesRefresh.Day)
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
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You have not spent any **Skill Points**, so you have nothing to reset.").Build())
                  .ConfigureAwait(false);
                return;
            }

            if (profile.Gems < 50)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You have **{profile.Gems}** {EmojiHandler.GetEmoji("gem")}," +
                   $" but you need **50** {EmojiHandler.GetEmoji("gem")} to reset your **Skill Points**.").Build())
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
            profile.Gems -= 50;

            await _profileService.Update(ctx, profile);

            await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully reset all your **Skill Points** by using **50** {EmojiHandler.GetEmoji("gem")}.").Build()).ConfigureAwait(false);
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

                        await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully upgraded **{ProfileSkillTypeEnum.DPS}** to level **{profile.DPSBoostLevel}** by using **{skillCost}** Skill Points.").Build()).ConfigureAwait(false);
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You have **{skillPoints}** Skill Points," +
                    $" but you need **{skillCost}** Skill Points to upgrade **{ProfileSkillTypeEnum.DPS}**.").Build())
                .ConfigureAwait(false);
                    }
                    break;
                case ProfileSkillTypeEnum.HP:
                    skillCost = Math.Round(1 * Math.Pow(profile.BoostCostIncrease, profile.HPBoostLevel - 1), 0);

                    if (skillCost <= skillPoints)
                    {
                        profile.SkillPointsSpent += skillCost;
                        profile.HPBoostLevel++;

                        await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully upgraded **{ProfileSkillTypeEnum.HP}** to level **{profile.HPBoostLevel}** by using **{skillCost}** Skill Points.").Build()).ConfigureAwait(false);
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You have **{skillPoints}** Skill Points," +
                    $" but you need **{skillCost}** Skill Points to upgrade **{ProfileSkillTypeEnum.HP}**.").Build())
                .ConfigureAwait(false);
                    }
                    break;
                case ProfileSkillTypeEnum.Armor:
                    skillCost = Math.Round(1 * Math.Pow(profile.BoostCostIncrease, profile.ArmorBoostLevel - 1), 0);

                    if (skillCost <= skillPoints)
                    {
                        profile.SkillPointsSpent += skillCost;
                        profile.ArmorBoostLevel++;

                        await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully upgraded **{ProfileSkillTypeEnum.Armor}** to level **{profile.ArmorBoostLevel}** by using **{skillCost}** Skill Points.").Build()).ConfigureAwait(false);
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You have **{skillPoints}** Skill Points," +
                    $" but you need **{skillCost}** Skill Points to upgrade **{ProfileSkillTypeEnum.Armor}**.").Build())
                .ConfigureAwait(false);
                    }
                    break;
                case ProfileSkillTypeEnum.Accuracy:
                    skillCost = Math.Round(1 * Math.Pow(profile.BoostCostIncrease, profile.AccuracyBoostLevel - 1), 0);

                    if (skillCost <= skillPoints)
                    {
                        profile.SkillPointsSpent += skillCost;
                        profile.AccuracyBoostLevel++;

                        await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully upgraded **{ProfileSkillTypeEnum.Accuracy}** to level **{profile.AccuracyBoostLevel}** by using **{skillCost}** Skill Points.").Build()).ConfigureAwait(false);
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You have **{skillPoints}** Skill Points," +
                    $" but you need **{skillCost}** Skill Points to upgrade **{ProfileSkillTypeEnum.Accuracy}**.").Build())
                .ConfigureAwait(false);
                    }
                    break;
                case ProfileSkillTypeEnum.Agility:
                    skillCost = Math.Round(1 * Math.Pow(profile.BoostCostIncrease, profile.AgilityBoostLevel - 1), 0);

                    if (skillCost <= skillPoints)
                    {
                        profile.SkillPointsSpent += skillCost;
                        profile.AgilityBoostLevel++;

                        await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully upgraded **{ProfileSkillTypeEnum.Agility}** to level **{profile.AgilityBoostLevel}** by using **{skillCost}** Skill Points.").Build()).ConfigureAwait(false);
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You have **{skillPoints}** Skill Points," +
                    $" but you need **{skillCost}** Skill Points to upgrade **{ProfileSkillTypeEnum.Agility}**.").Build())
                .ConfigureAwait(false);
                    }
                    break;
            }

            await _profileService.Update(ctx, profile);
        }
    }
}
