using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using IdleHeroesDAL;
using System.Threading.Tasks;
using IdleHeroes.Support;
using System;
using IdleHeroes.Models;
using IdleHeroes.EmbedTemplates;

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
            try
            {
                var latency = ctx.Client.Ping;
                await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"Pong! `{latency}ms`").Build())
                    .ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                if(BotSettings.IsDebugMode)
                {
                    await ctx.Channel.SendMessageAsync(embed: ErrorEmbedTemplate.Get(ctx, $"COMMAND ERROR: {ex.Message}").Build())
                    .ConfigureAwait(false);
                }
                Console.WriteLine($"COMMAND ERROR: {ex.Message}");
            }
            
        }

        [Command("die")]
        [Description("Kills the bot.")]
        public async Task KillBot(CommandContext ctx)
        {
            try
            {
                if (UtilityFunctions.IsBotOnwer(ctx.Message.Author.Id))
                {
                    await ctx.Client.DisconnectAsync();
                    return;
                }

                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, "This command is restricted to the **Owners** of the bot only!").Build())
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
    }
}
