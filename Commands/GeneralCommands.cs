using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Idle_Heroes.Commands
{
    public class GeneralCommands : BaseCommandModule
    {
        [Command("ping")]
        [Description("Test the latency of the bot.")]
        public async Task Ping(CommandContext ctx)
        {
            var latency =  DateTime.Now.Millisecond - ctx.Message.CreationTimestamp.Millisecond;

            await ctx.Channel.SendMessageAsync($"Pong! {latency}ms")
                .ConfigureAwait(false);
        }
    }
}
