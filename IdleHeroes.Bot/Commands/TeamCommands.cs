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
using System.Linq;
using System.Threading.Tasks;

namespace IdleHeroes.Commands
{
    public class TeamCommands : BaseCommandModule
    {
        ICompanionService _companionService;
        IProfileService _profileService;

        public TeamCommands(ICompanionService companionService, IProfileService profileService)
        {
            _companionService = companionService;
            _profileService = profileService;
        }

        [Command("team")]
        [Description("Preview and manage your team.")]
        public async Task CompanionCodex(CommandContext ctx, [Description("The position of the team you want to select.")] string teamPosition = null, [Description("The Companion ID you want to put in the position selected. You can also type `hero` to place your hero in that position. Typing another position here, will swap the two positions with each other.")] string companionId = null)
        {
            try
            {
                Profile profile = await _profileService.FindByDiscordId(ctx);

                if (!string.IsNullOrEmpty(teamPosition))
                {
                    if (!Enum.TryParse(teamPosition.ToUpper(), out TeamPositionEnum tPosition) || int.TryParse(teamPosition, out int n))
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"Team position **{teamPosition}** does not exist. Use `.help team` to find out more.").Build())
                .ConfigureAwait(false);
                    }
                    else if (string.IsNullOrEmpty(companionId))
                    {
                        await ClearPosition(ctx, profile, tPosition);
                    }
                    else
                    {
                        int compId;
                        try
                        {
                            compId = Convert.ToInt32(companionId);
                            await AddToPosition(ctx, profile, tPosition, compId);
                        }
                        catch (Exception ex)
                        {
                            if(companionId == "hero")
                            {
                                await AddHeroToPosition(ctx, profile, tPosition);
                            }
                            //Switc places
                            else if (Enum.TryParse(companionId.ToUpper(), out TeamPositionEnum tPosition2) && !int.TryParse(companionId, out int n2) && !int.TryParse(teamPosition, out int n3))
                            {
                                await SwapPositions(ctx, profile, tPosition, tPosition2);
                            }
                            else
                            {
                                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"Please make sure you have used the correct command syntax. Use `.help team` to find out more.").Build()).ConfigureAwait(false);
                                return;
                            }
                        }
                    }
                }

                await ctx.Channel.SendMessageAsync(embed: TeamEmbedTemplate.Show(ctx, profile).Build())
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

        private async Task AddHeroToPosition(CommandContext ctx, Profile profile, TeamPositionEnum tPosition)
        {
            profile.Team.HeroTeamPosition = tPosition;

            TeamCompanion teamCompanionToRemove = profile.Team.Companions.Find(x => x.TeamPosition == tPosition);

            profile.Team.Companions.Remove(teamCompanionToRemove);

            await _profileService.Update(ctx, profile);

            await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully moved your hero to position **{tPosition}**.").Build())
                   .ConfigureAwait(false);
        }

        private async Task SwapPositions(CommandContext ctx, Profile profile, TeamPositionEnum tPosition, TeamPositionEnum tPosition2)
        {
            TeamCompanion companionPosition1 = profile.Team.Companions.Find(x => x.TeamPosition == tPosition);
            TeamCompanion companionPosition2 = profile.Team.Companions.Find(x => x.TeamPosition == tPosition2);

            if(profile.Team.HeroTeamPosition == tPosition)
            {
                profile.Team.HeroTeamPosition = tPosition2;
                profile.Team.Companions.Remove(companionPosition2);
            }
            else if (profile.Team.HeroTeamPosition == tPosition2)
            {
                profile.Team.HeroTeamPosition = tPosition;
                profile.Team.Companions.Remove(companionPosition1);
            }

            if (companionPosition1 == null && companionPosition2 == null)
            {
                await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully swapped position between position **{tPosition}** and **{tPosition2}**.").Build())
                   .ConfigureAwait(false);
                return;
            }

            if (companionPosition1 != null)
            {
                TeamCompanion companionToAdd1 = new TeamCompanion()
                {
                    OwnedCompanion = companionPosition1.OwnedCompanion,
                    TeamPosition = tPosition2
                };

                profile.Team.Companions.Remove(companionPosition1);
                profile.Team.Companions.Add(companionToAdd1);
            }
            else
            {
                profile.Team.Companions.Remove(companionPosition1);
            }

            if (companionPosition2 != null)
            {
                TeamCompanion companionToAdd2 = new TeamCompanion()
                {
                    OwnedCompanion = companionPosition2.OwnedCompanion,
                    TeamPosition = tPosition
                };

                profile.Team.Companions.Remove(companionPosition2);
                profile.Team.Companions.Add(companionToAdd2);
            }
            else
            {
                profile.Team.Companions.Remove(companionPosition2);
            }
            

            await _profileService.Update(ctx, profile);

            await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully swapped position between position **{tPosition}** and **{tPosition2}**.").Build())
                   .ConfigureAwait(false);
        }

        private async Task AddToPosition(CommandContext ctx, Profile profile, TeamPositionEnum tPosition, int compId)
        {
            if (profile.Team.HeroTeamPosition == tPosition)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"Position **{tPosition}** is taken by your **Hero**. You need to move your **Hero** first.").Build())
                .ConfigureAwait(false);
                return;
            }

            TeamCompanion companionToBeReplaced = profile.Team.Companions.Find(x => x.TeamPosition == tPosition);
            OwnedCompanions companionToPlace = profile.OwnedCompanions.Find(x => x.Companion.Id == compId);

            if(companionToPlace == null)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You do not own any Companions with ID **{compId}**.").Build())
                .ConfigureAwait(false);
                return;
            }

            List<TeamCompanion> companionsToRemove = profile.Team.Companions.FindAll(x => x.OwnedCompanion.Companion.Id == compId);
            foreach(TeamCompanion teamCompanion in companionsToRemove)
            {
                profile.Team.Companions.Remove(teamCompanion);
            }

            if(companionToBeReplaced != null)
            {
                companionToBeReplaced.OwnedCompanion = companionToPlace;
            }
            else
            {
                TeamCompanion teamCompanion = new TeamCompanion()
                {
                    OwnedCompanion = companionToPlace,
                    TeamPosition = tPosition
                };
                profile.Team.Companions.Add(teamCompanion);
            }

            await _profileService.Update(ctx, profile);

            await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"{EmojiHandler.GetEmoji(companionToPlace.Companion.IconName)} **{companionToPlace.Companion.Name}** has been placed in team position **{tPosition}**.").Build())
                   .ConfigureAwait(false);
        }

        private async Task ClearPosition(CommandContext ctx, Profile profile, TeamPositionEnum tPosition)
        {
            if (profile.Team.HeroTeamPosition == tPosition)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"The **Hero** cannot be removed from the Team. You can only move the **Hero** to another position.").Build())
                .ConfigureAwait(false);
                return;
            }

            TeamCompanion companionToRemove = profile.Team.Companions.Find(x => x.TeamPosition == tPosition);

            if (companionToRemove != null)
            {
                profile.Team.Companions.Remove(companionToRemove);

                await _profileService.Update(ctx, profile);

                await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"{EmojiHandler.GetEmoji(companionToRemove.OwnedCompanion.Companion.IconName)} **{companionToRemove.OwnedCompanion.Companion.Name}** has been removed from the Team.").Build())
                   .ConfigureAwait(false);
                return;
            }

            await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"Team position **{tPosition}** is already empty.").Build())
                .ConfigureAwait(false);
        }
    }
}
