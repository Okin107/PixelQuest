using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using IdleHeroesDAL;
using IdleHeroesDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using IdleHeroes.Models;
using Newtonsoft.Json;

namespace IdleHeroes.Commands
{
    public class GeneralCommands : BaseCommandModule
    {
        private readonly DatabaseContext _context;

        public GeneralCommands(DatabaseContext context)
        {
            _context = context;
        }

        [Command("ping")]
        [Description("Test the latency of the bot.")]
        public async Task Ping(CommandContext ctx)
        {
            var latency = ctx.Client.Ping;

            await ctx.Channel.SendMessageAsync($"`Pong! {latency}ms`")
                .ConfigureAwait(false);
        }

        [Command("die")]
        [Description("Kills the bot.")]
        public async Task KillBot(CommandContext ctx)
        {
            string configString;

            await using (var fs = File.OpenRead("config.json"))
            {
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                {
                    configString = await sr.ReadToEndAsync();
                }
            }
            
            ConfigModel configJson = JsonConvert.DeserializeObject<ConfigModel>(configString);

            foreach (ulong userId in configJson.BotOwners)
            {
                if (userId == ctx.Message.Author.Id)
                {
                    await ctx.Client.DisconnectAsync();
                }
            }
        }

        [Command("create")]
        [Description("Create a new profile for the game.")]
        public async Task Create(CommandContext ctx, [Description("The display name for your profile.")] string username = null)
        {
            //Check if username is empty
            if(String.IsNullOrEmpty(username))
            {
                await ctx.Channel.SendMessageAsync($"`Your username cannot be empty. Please select a proper name for your profile.`")
                .ConfigureAwait(false);
                return;
            }

            //Check if profile exists
            Profile profile = await _context.Profile.FirstOrDefaultAsync(x => x.DiscordID.Equals(ctx.Member.Id));

            if(profile != null)
            {
                await ctx.Channel.SendMessageAsync($"`You already have a profile.`")
                .ConfigureAwait(false);
                return;
            }

            await _context.Profile.AddAsync(new Profile()
            {
                Username = username,
                DiscordID = ctx.Member.Id
            }).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync($"`Your new profile was created.`")
                .ConfigureAwait(false);
        }

        [Command("profile")]
        [Description("View your profile or the profile of another player.")]
        public async Task Profile(CommandContext ctx, [Description("The username you want to view the profile for. Leaving this empty will show your own profile.")] string username = null)
        {
            Profile profile;

            //TODO: Turn this into multiple selection if there are many results. User interactivity methods
            if (!string.IsNullOrEmpty(username))
            {
                profile = await _context.Profile.FirstOrDefaultAsync(x => x.Username.Equals(username));
            }
            else
            {
                profile = await _context.Profile.FirstOrDefaultAsync(x => x.DiscordID.Equals(ctx.Member.Id));
            }

            if (profile == null) 
            {
                await ctx.Channel.SendMessageAsync($"`There is no profile with that username.`")
                    .ConfigureAwait(false);
            }
            else
            {
                await ctx.Channel.SendMessageAsync($"`This is the user profile of **{profile.Username}**`")
            .ConfigureAwait(false);
            }
        }
    }
}
