using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using IdleHeroes.EmbedTemplates;
using IdleHeroes.Models;
using IdleHeroes.Services;
using IdleHeroesDAL.Models;
using System;
using System.Threading.Tasks;

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

    }
}
